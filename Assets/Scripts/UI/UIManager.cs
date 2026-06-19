using UnityEngine;
using UnityEngine.UI;
using VeggieJump.Core;

namespace VeggieJump.UI
{
    /// <summary>
    /// Wires the GameManager's score/game-over events to on-screen UI.
    /// Attach to a UIManager GameObject under your Canvas and drag in
    /// the relevant UI references in the Inspector.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("HUD")]
        [SerializeField] private Text scoreText; // swap for TMP_Text if using TextMeshPro

        [Header("Game Over Panel")]
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Text finalScoreText;
        [SerializeField] private Button restartButton;

        private void Start()
        {
            gameOverPanel.SetActive(false);

            GameManager.Instance.OnScoreChanged += UpdateScoreText;
            GameManager.Instance.OnGameOver += ShowGameOverPanel;

            restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
        }

        private void UpdateScoreText(int score)
        {
            scoreText.text = score.ToString();
        }

        private void ShowGameOverPanel()
        {
            finalScoreText.text = $"Score: {GameManager.Instance.Score}";
            gameOverPanel.SetActive(true);
        }

        private void OnDestroy()
        {
            if (GameManager.Instance == null) return;
            GameManager.Instance.OnScoreChanged -= UpdateScoreText;
            GameManager.Instance.OnGameOver -= ShowGameOverPanel;
        }
    }
}
