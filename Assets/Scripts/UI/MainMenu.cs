using UnityEngine;
using Utils;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        // function that can receive animation events
        // function to play fade

        [SerializeField] private Animation mainMenuAnimator;
        [SerializeField] private AnimationClip fadeInAnimation;
        [SerializeField] private AnimationClip fadeOutAnimation;

        public Events.EventFadeComplete OnMainMenuFadeComplete;

        private void Start()
        {
            GameManager.Instance.OnGameStateChanaged.AddListener(HandleGameStateChanged);
        }

        private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
        {
            if (mainMenuAnimator == null) return;
            if (previousState == GameManager.GameState.Pregame && currentState != GameManager.GameState.Pregame)
            {
                FadeOut();
            }

            if (previousState != GameManager.GameState.Pregame && currentState == GameManager.GameState.Pregame)
            {
                FadeIn();
            }
        }

        /// <summary>
        /// Will be trggered by MainMenuFadeIn animation
        /// </summary>
        public void OnFadeInComplete()
        {
            OnMainMenuFadeComplete.Invoke(false);
            UIManager.Instance.SetDummyCameraActive(true);
        }

        /// <summary>
        /// Will be trggered by MainMenuFadeOut animation
        /// </summary>
        public void OnFadeOutComplete()
        {
            OnMainMenuFadeComplete.Invoke(true);
        }

        public void FadeIn()
        {
            mainMenuAnimator.Stop();
            mainMenuAnimator.clip = fadeInAnimation;
            mainMenuAnimator.Play();
        }

        public void FadeOut()
        {
            UIManager.Instance.SetDummyCameraActive(false);
            mainMenuAnimator.Stop();
            mainMenuAnimator.clip = fadeOutAnimation;
            mainMenuAnimator.Play();
        }
    }
}