using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public TMP_Text scoreText;
    public float score;
    public float scoreRate = 5f; // Her saniye kaç puan
    private bool isCounting = true;

    [Header("Difficulty Scaling")]
    public float difficultyStep = 100f;         // Kaç puanda bir zorluk artsın
    public float speedMultiplier = 1.05f;       // Her adımda hız çarpanı
    private float nextDifficultyScore = 100f;

    private MovementSystem movementSystem;

    void Start()
    {
        movementSystem = FindObjectOfType<MovementSystem>();
    }

    void Update()
    {
        if (isCounting)
        {
            score += scoreRate * Time.deltaTime;
            UpdateUI();

            // ✅ Hız artırma mantığı
            if (score >= nextDifficultyScore)
            {
                nextDifficultyScore += difficultyStep;

                if (movementSystem != null)
                {
                    movementSystem.IncreaseSpeed(speedMultiplier);
                    Debug.Log("🔥 Speed increased at score: " + score);
                }
            }
        }
    }

    public void ResetScore()
    {
        score = 0;
        nextDifficultyScore = difficultyStep;
        isCounting = true;
        UpdateUI();
    }

    public void StopScoring()
    {
        isCounting = false;
    }

    public void StartScoring()
    {
        isCounting = true;
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }

    public int GetFinalScore()
    {
        return Mathf.FloorToInt(score);
    }
}
