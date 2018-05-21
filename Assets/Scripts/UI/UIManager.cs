using System;
using UnityEngine;
using Utils;

namespace UI
{
    /// <summary>
    /// Manager of UI, like main menu and pause menu
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private MainMenu mainMenu;
        [SerializeField] private PauseMenu pauseMenu;
        [SerializeField] private Camera dummyCamera;

        // As a main entry for this events
        public Events.EventFadeComplete OnMainMenuFadeComplete;

        private void Start()
        {
            // Listen to MainMenu, then other Manager can listen this event from UImanager instead, to structure the relation between codes.
            mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
            GameManager.Instance.OnGameStateChanaged.AddListener(HandleGameStateChanged);
        }

        private void HandleMainMenuFadeComplete(bool fadeOut)
        {
            OnMainMenuFadeComplete.Invoke(fadeOut);
        }

        private void Update()
        {
            if (GameManager.Instance.CurrentGameState == GameManager.GameState.Pregame)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameManager.Instance.StartGame();
                }
            }
        }

        public void SetDummyCameraActive(bool active)
        {
            dummyCamera.gameObject.SetActive(active);
        }

        /// <summary>
        /// Callback when game state change, manage relevant UI
        /// </summary>
        /// <param name="currentState">Current game state</param>
        /// <param name="previousState">previous game state</param>
        private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
        {
            switch (currentState)
            {
                case GameManager.GameState.Paused:
                    pauseMenu.gameObject.SetActive(true);
                    break;
                default:
                    pauseMenu.gameObject.SetActive(false);
                    break;
            }
        }
    }
}