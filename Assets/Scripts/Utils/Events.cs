using System;
using UnityEngine.Events;

namespace Utils
{
    /// <summary>
    /// Store all of the events in game
    /// </summary>
    public class Events
    {
        [Serializable]
        public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState>
        {
        }

        /// <summary>
        /// if listen to fade out, receive true
        /// if listen to fade in, receive false
        /// </summary>
        [Serializable]
        public class EventFadeComplete : UnityEvent<bool>
        {
        }
    }
}