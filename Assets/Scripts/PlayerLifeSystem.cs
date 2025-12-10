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

    public GameObject dizzyEffectPrefab;  
    private GameObject activeDizzyEffect;

    [Header("UI")]
    public Image healthBarImage;
    private void Awake()
    {
        movementSystem = GetComponent<MovementSystem>();
        respawner = GetComponent<PlayerRespawnSystem>();

        ResetLives(); // ⛑️ En başta çağrılır
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

        ShowDizzyEffect();  // ✅ Dizzy stars above head

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
        yield return new WaitForSeconds(1f);
        movementSystem.RestoreSpeed();
        isSlowingDown = false;
    }

    private void ShowDizzyEffect()
    {
        if (dizzyEffectPrefab == null) return;

        // Clear existing effect if any
        if (activeDizzyEffect != null)
            Destroy(activeDizzyEffect);

        // Spawn at head position
        Vector3 headPosition = transform.position + new Vector3(0, 2f, 0);
        activeDizzyEffect = Instantiate(dizzyEffectPrefab, headPosition, Quaternion.identity, transform);

        StartCoroutine(RemoveDizzyEffectAfterSeconds(2f));
    }

    private IEnumerator RemoveDizzyEffectAfterSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (activeDizzyEffect != null)
            Destroy(activeDizzyEffect);
    }

}
