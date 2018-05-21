using System;

namespace InventorySystem
{
    // TODO
    public enum TemporaryBuff
    {
        Untouchable,
        ReduceDamage,
        None,
    }

    /// <summary>
    /// Consumable Item, subclass of Item
    /// </summary>
    public class Consumable : Item
    {
        public int Hp;
        public TemporaryBuff TemporaryBuff;

        public Consumable(int id, string name, ItemType type, string description, string flavorText, int capacity,
            int buyPrice, int sellPrice, string sprite, int hp, TemporaryBuff temporaryBuff) : base(id, name, type,
            description,
            flavorText, capacity, buyPrice, sellPrice, sprite)
        {
            Hp = hp;
            TemporaryBuff = temporaryBuff;
        }

        public override string GetToolTipText()
        {
            string text = base.GetToolTipText();
            string newText = String.Empty;
            if (Hp > 0)
            {
                newText = string.Format("{0}\n\nHeal: {1}", text, Hp);
            }
            else if (TemporaryBuff != 0)
            {
                // this item can add buff to player
                newText = string.Format("{0}\n\nBuff: {1}", text, TemporaryBuff.ToString());
            }
            else if (Hp > 0 && TemporaryBuff != 0)
            {
                newText = string.Format("{0}\n\nHeal: {1}\nBuff: {2}", text, Hp, TemporaryBuff.ToString());
            }

            return newText;
        }

        public override string ToString()
        {
            string s = String.Empty;
            s += Id.ToString();
            s += Name;
            s += Description;
            s += FlavorText;
            s += Capacity;
            s += Sprite;
            s += Hp;
            s += TemporaryBuff.ToString();
            return s;
        }
    }
}