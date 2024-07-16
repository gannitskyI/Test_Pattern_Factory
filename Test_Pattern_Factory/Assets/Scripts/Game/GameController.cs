using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private int minEnemiesToDestroy = 10;
    [SerializeField] private int maxEnemiesToDestroy = 20;

    private int enemiesToDestroy;
    private int destroyedEnemiesCount;

    private UIManager uiManager;
    private GameOverUI gameOverUI;

    void Start()
    {
        // Определение случайного числа врагов для победы
        enemiesToDestroy = Random.Range(minEnemiesToDestroy, maxEnemiesToDestroy + 1);
        destroyedEnemiesCount = 0;

        // Получаем ссылки на UIManager и GameOverUI
        uiManager = FindObjectOfType<UIManager>();
        gameOverUI = FindObjectOfType<GameOverUI>();

        // Подписываемся на событие уничтожения врага
        Enemy.OnEnemyDestroyed += OnEnemyDestroyed;

        // Обновляем текст с количеством врагов для уничтожения
        if (uiManager != null)
        {
            uiManager.UpdateEnemiesToDestroyText(enemiesToDestroy);
        }
    }

    private void OnEnemyDestroyed(Enemy enemy)
    {
        destroyedEnemiesCount++;

        // Обновляем текст с количеством оставшихся врагов для уничтожения
        if (uiManager != null)
        {
            int remainingEnemies = enemiesToDestroy - destroyedEnemiesCount;
            uiManager.UpdateEnemiesToDestroyText(remainingEnemies);
        }

        // Проверяем условие победы
        if (destroyedEnemiesCount >= enemiesToDestroy)
        {
            if (gameOverUI != null)
            {
                gameOverUI.ShowVictory();
            }
        }
    }

    public int GetEnemiesToDestroy()
    {
        return enemiesToDestroy;
    }

    void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        Enemy.OnEnemyDestroyed -= OnEnemyDestroyed;
    }
}
