using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLives = 5;
    private int currentLives;

    public event Action<int, int> OnLivesChanged;

    private GameOverUI gameOverUI;

    private void Awake()
    {
        currentLives = maxLives;
    }

    private void Start()
    {
        gameOverUI = FindObjectOfType<GameOverUI>();
        UpdateLivesUI();
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        if (currentLives <= 0)
        {
            currentLives = 0;
            GameOver();
        }
        UpdateLivesUI();
    }

    private void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOver();
           
        }
    }

    private void UpdateLivesUI()
    {
        OnLivesChanged?.Invoke(currentLives, maxLives);
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }

    public int GetMaxLives()
    {
        return maxLives;
    }
}
