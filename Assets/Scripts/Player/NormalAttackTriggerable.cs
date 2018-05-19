using System;
using UnityEngine;

namespace Player
{
    public class NormalAttackTriggerable : MonoBehaviour
    {
        public Transform PlayerPos;
        public int Damage;
        public string Description;
        public Talent Talent = Talent.Fire;
        private WaitForSeconds durationWait;

        public void Initialize()
        {
            PlayerPos = transform;

        }
        public void Trigger()
        {
            Debug.Log("Throw a fire ball");
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