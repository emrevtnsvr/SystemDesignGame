using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeSystem : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    private MovementSystem movementSystem;
    private PlayerRespawnSystem respawner;

    private bool isSlowingDown = false;

    [Header("UI")]
    public Image healthBarImage; // Fill-type image

    private void Awake()
    {
        movementSystem = GetComponent<MovementSystem>();
        respawner = GetComponent<PlayerRespawnSystem>();

        ResetLives(); // ⛑️ En başta çağrılır
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blackhole"))
        {
            Debug.Log("🕳️ Blackhole detected → player out of lives");

            currentLives = 0;
            UpdateHealthUI();
            FindObjectOfType<ScoreSystem>()?.StopScoring();
            respawner.RespawnPlayer(); // Bu zaten blackhole'u da resetliyor
        }
    }

    public void TakeDamage()
    {
        currentLives--;
        Debug.Log("Player damaged! Lives left: " + currentLives);

        UpdateHealthUI();

        if (!isSlowingDown)
            StartCoroutine(ApplyTemporarySlowdown());

        if (currentLives <= 0)
        {
            Debug.Log("Out of lives! Respawning at checkpoint...");

            FindObjectOfType<ScoreSystem>()?.StopScoring();

            respawner.RespawnPlayer();
        }
    }

    public void ResetLives()
    {
        currentLives = maxLives;
        Debug.Log("Lives reset to max.");

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = (float)currentLives / maxLives;
        }
    }

    private System.Collections.IEnumerator ApplyTemporarySlowdown()
    {
        isSlowingDown = true;

        movementSystem.SetSlowdown(0.7f);
        yield return new WaitForSeconds(2f);
        movementSystem.RestoreSpeed();
        isSlowingDown = false;
    }


}
