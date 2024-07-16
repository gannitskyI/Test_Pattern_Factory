using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;  // Подключаем DoTween

public class GameOverUI : MonoBehaviour, IGameOverUI
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;

    void Start()
    {
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        AnimatePanel(gameOverPanel, StopGame);
    }

    public void ShowVictory()
    {
        victoryPanel.SetActive(true);
        AnimatePanel(victoryPanel, StopGame);
    }

    private void AnimatePanel(GameObject panel, TweenCallback onComplete)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = panel.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0;
        panel.transform.localScale = Vector3.zero;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(panel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
        sequence.Join(canvasGroup.DOFade(1, 0.5f));
        sequence.OnComplete(onComplete);  // Устанавливаем callback на завершение анимации
        sequence.Play();
    }

    private void StopGame()
    {
        Time.timeScale = 0;  // Останавливаем время в игре
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;  // Восстанавливаем нормальное течение времени
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
