using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI enemiesToDestroyText;

    private PlayerHealth playerHealth;
    private GameController gameController;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        SubscribeEvents();

        if (playerHealth != null)
        {
            UpdateLivesText(playerHealth.GetCurrentLives(), playerHealth.GetMaxLives());
        }

        if (gameController != null)
        {
            UpdateEnemiesToDestroyText(gameController.GetEnemiesToDestroy());
        }
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        if (playerHealth != null)
        {
            playerHealth.OnLivesChanged += UpdateLivesText;
        }
    }

    private void UnsubscribeEvents()
    {
        if (playerHealth != null)
        {
            playerHealth.OnLivesChanged -= UpdateLivesText;
        }
    }

    private void UpdateLivesText(int currentLives, int maxLives)
    {
        if (livesText != null)
        {
            livesText.text = $"Жизней: {currentLives}/{maxLives}";
        }
    }

    public void UpdateEnemiesToDestroyText(int enemiesToDestroy)
    {
        if (enemiesToDestroyText != null)
        {
            enemiesToDestroyText.text = $"Осталось уничтожить: {enemiesToDestroy}";
        }
    }
}
