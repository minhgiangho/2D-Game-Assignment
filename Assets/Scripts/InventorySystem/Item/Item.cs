namespace InventorySystem
{
    /// <summary>
    /// Item Type Enum
    /// </summary>
    public enum ItemType
    {
        Consumable,
        Relic,
    }

    /// <summary>
    /// Base class of item
    /// </summary>
    public class Item
    {
        public int Id;
        public string Name;
        public ItemType Type;

        /// <summary>
        /// like +10 hp
        /// </summary>
        public string Description;

        /// <summary>
        /// Text for action figure item backgrounds, for fun and let player dive into story
        /// </summary>
        public string FlavorText;

        public int Capacity;
        public int BuyPrice;
        public int SellPrice;

        /// <summary>
        /// Path of sprite (icon)
        /// </summary>
        public string Sprite;

        public Item()
        {
            Id = -1;
        }

        public Item(int id, string name, ItemType type, string description, string flavorText, int capacity,
            int buyPrice, int sellPrice, string sprite)
        {
            Id = id;
            Name = name;
            Type = type;
            Description = description;
            FlavorText = flavorText;
            Capacity = capacity;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            Sprite = sprite;
        }

        public virtual string GetToolTipText()
        {
            // or change color?
            string text = string.Format("{0}\nBuy Price: {1}\nSell Price: {2}\n{3}\n{4}", Name, BuyPrice, SellPrice,
                Description, FlavorText);
            return text;
        }
    }
}