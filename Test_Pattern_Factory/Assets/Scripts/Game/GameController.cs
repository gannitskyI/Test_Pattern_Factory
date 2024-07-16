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
        // ����������� ���������� ����� ������ ��� ������
        enemiesToDestroy = Random.Range(minEnemiesToDestroy, maxEnemiesToDestroy + 1);
        destroyedEnemiesCount = 0;

        // �������� ������ �� UIManager � GameOverUI
        uiManager = FindObjectOfType<UIManager>();
        gameOverUI = FindObjectOfType<GameOverUI>();

        // ������������� �� ������� ����������� �����
        Enemy.OnEnemyDestroyed += OnEnemyDestroyed;

        // ��������� ����� � ����������� ������ ��� �����������
        if (uiManager != null)
        {
            uiManager.UpdateEnemiesToDestroyText(enemiesToDestroy);
        }
    }

    private void OnEnemyDestroyed(Enemy enemy)
    {
        destroyedEnemiesCount++;

        // ��������� ����� � ����������� ���������� ������ ��� �����������
        if (uiManager != null)
        {
            int remainingEnemies = enemiesToDestroy - destroyedEnemiesCount;
            uiManager.UpdateEnemiesToDestroyText(remainingEnemies);
        }

        // ��������� ������� ������
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
        // ������������ �� ������� ��� ����������� �������
        Enemy.OnEnemyDestroyed -= OnEnemyDestroyed;
    }
}
