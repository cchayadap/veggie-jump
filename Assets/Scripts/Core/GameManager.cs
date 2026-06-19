using UnityEngine;
using UnityEngine.SceneManagement;
using VeggieJump.Player;

namespace VeggieJump.Core
{
    /// <summary>
    /// Central game state: tracks score (based on max height climbed),
    /// handles game over and restart. Attach to an empty "GameManager"
    /// GameObject that persists for the scene.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private PlayerController player;

        public int Score { get; private set; }
        public bool IsGameOver { get; private set; }

        /// Fired whenever score changes - UI can subscribe to this.
        public System.Action<int> OnScoreChanged;
        /// Fired once when the game ends - UI can subscribe to show the game over panel.
        public System.Action OnGameOver;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            if (IsGameOver || player == null) return;

            int heightScore = Mathf.FloorToInt(player.HighestY * 10f);
            if (heightScore > Score)
            {
                Score = heightScore;
                OnScoreChanged?.Invoke(Score);
            }
        }

        public void GameOver()
        {
            if (IsGameOver) return;
            IsGameOver = true;
            Time.timeScale = 0f; // freeze gameplay
            OnGameOver?.Invoke();
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
