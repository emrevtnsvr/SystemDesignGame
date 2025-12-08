using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public TMP_Text scoreText;
    public float score;
    public float scoreRate = 5f; // her saniye kaç puan artsın

    private bool isCounting = true;

    void Update()
    {
        if (isCounting)
        {
            score += scoreRate * Time.deltaTime;
            UpdateUI();
        }
    }

    public void ResetScore()
    {
        score = 0;
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
