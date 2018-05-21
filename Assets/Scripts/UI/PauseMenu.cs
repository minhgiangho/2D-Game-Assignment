using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Pause Menu show whenever press Esc key
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        public Button ResumeButton;
        public Button QuitButton;
        public Button RestartButton;

        private void Awake()
        {
            ResumeButton.onClick.AddListener(HandleResumeClick);
            QuitButton.onClick.AddListener(HandleQuitClick);
            RestartButton.onClick.AddListener(HandleRestartClick);
        }

        private void HandleResumeClick()
        {
            GameManager.Instance.TogglePause();
        }

        private void HandleQuitClick()
        {
            GameManager.Instance.QuitGame();
        }

        private void HandleRestartClick()
        {
            GameManager.Instance.RestartGame();
        }
    }
}