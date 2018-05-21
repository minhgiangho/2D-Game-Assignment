using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    /// <summary>
    /// Main entry of player inventory system
    /// TODO: combine with PlayerStat.cs
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {
        private int basicStrength = 10;
        private int basicDamage = 10;

        public int BasicStrength
        {
            get { return basicStrength; }
        }
        // TODO
        public int BasicDamage
        {
            get { return basicDamage; }
        }

        private int coinAmount = 0;
        private Text coinText;

        public int CoinAmount
        {
            get { return coinAmount; }
            set
            {
                coinAmount = value;
                coinText.text = coinAmount.ToString();
            }
        }

        private void Start()
        {
            coinText = GameObject.Find("Coin").GetComponentInChildren<Text>();
            coinText.text = coinAmount.ToString();
        }

        private void Update()
        {
        }

        /// <summary>
        /// Can player afford this transaction, if yes, consume amount coins
        /// </summary>
        /// <param name="amount">Transaction amount</param>
        /// <returns>if exchange successfully, return true</returns>
        public bool CanConsumeCoin(int amount)
        {
            if (coinAmount >= amount)
            {
                coinAmount -= amount;
                coinText.text = coinAmount.ToString();
                return true;
            }

            return false;
        }

        /// <summary>
        /// player get coin
        /// </summary>
        public void EarnCoin(int amount)
        {
            coinAmount += amount;
            coinText.text = coinAmount.ToString();
        }
    }
}