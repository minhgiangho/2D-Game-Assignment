using System;
using UnityEngine;

namespace Player
{
    public class ShieldTriggerable : MonoBehaviour
    {
        public Talent Talent = Talent.Fire;
        public int MaxHealth = 2;
        public float Duration = 1;
        public Transform PlayerPos;
        public string Description;

        private WaitForSeconds durationWait;

        public void Initialize()
        {

        }
        public void Trigger()
        {
            Debug.Log("Trigger Shield");
            // trigger shield
            switch (Talent)
            {
                case Talent.Fire:
                    break;
                case Talent.Ice:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}