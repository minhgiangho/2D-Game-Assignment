using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Player's main properties
    /// </summary>
//    [RequireComponent(typeof(PlayerTalent))]
    public class PlayerMain : MonoBehaviour
    {
        private float initHealth = 5;
        public bool IsAttacking { get; private set; }
        public Talent Talent { get; private set; }


        private void Awake()
        {
            Talent = Talent.Fire;

        }

        private IEnumerator Attack()
        {
            IsAttacking = true;

            // attack interval
            yield return new WaitForSeconds(1);

            // stop attack
        }
    }
    
    
}