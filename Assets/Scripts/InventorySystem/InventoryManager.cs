using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        private static InventoryManager instance;

        public static InventoryManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
                }

                return instance;
            }
        }

        /// <summary>
        /// All of the items in the game, read from JSON file
        /// </summary>
        private List<Item> itemList;

        // TODO: UI stuff
        private Canvas canvas;

        private void Awake()
        {
            ParseItemJson();
            Debug.Log(itemList[0]);
            Debug.Log(itemList[1]);
        }

        private void Update()
        {
        }

        // TODO: load data from boot scene
        /// <summary>
        /// Parse json file to get item list
        /// </summary>
        private void ParseItemJson()
        {
            itemList = new List<Item>();
            TextAsset itemText = Resources.Load<TextAsset>("Items");
            string itemsJson = itemText.text;
            JsonData jsonData = JsonMapper.ToObject(itemsJson);
            if (jsonData.IsArray)
            {
                foreach (JsonData data in jsonData)
                {
                    int id = (int) data["id"];
                    string name = data["name"].ToString();
                    string typeStr = data["type"].ToString();
                    ItemType type = (ItemType) System.Enum.Parse(typeof(ItemType), typeStr);
                    string description = data["description"].ToString();
                    string flavorText = data["flavorText"].ToString();
                    int capacity = (int) data["capacity"];
                    int buyPrice = (int) data["buyPrice"];
                    int sellPrice = (int) data["sellPrice"];
                    string sprite = data["sprite"].ToString();

                    Item item = null;
                    switch (type)
                    {
                        case ItemType.Consumable:
                            int hp = (int) data["hp"];
                            string buffStr = data["temporaryBuff"].ToString();

                            TemporaryBuff temporaryBuff =
                                (TemporaryBuff) System.Enum.Parse(typeof(TemporaryBuff), buffStr);
                            item = new Consumable(id, name, type, description, flavorText, capacity, buyPrice,
                                sellPrice, sprite, hp, temporaryBuff);
                            break;
                        case ItemType.Relic:
                            // TODO
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    itemList.Add(item);
                }
            }
        }

        /// <summary>
        /// Get item instance according to id
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>Item instance</returns>
        public Item GetItemById(int id)
        {
            foreach (Item item in itemList)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }
    }
}