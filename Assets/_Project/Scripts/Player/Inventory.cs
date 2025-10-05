using System.Collections.Generic;
using UnityEngine;

namespace NenNhangSinhMenh.Player
{
    /// <summary>
    /// Manages the player's inventory.
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        // Using a List to store item data. Simple and effective for this project.
        private List<Gameplay.ItemData> _items = new List<Gameplay.ItemData>();

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="item">The item data to add.</param>
        public void AddItem(Gameplay.ItemData item)
        {
            _items.Add(item);
            Debug.Log($"Added {item.itemName} to inventory.");
            // Later, we can add UI updates here.
        }

        /// <summary>
        /// Removes an item from the inventory.
        /// </summary>
        /// <param name="item">The item data to remove.</param>
        public void RemoveItem(Gameplay.ItemData item)
        {
            if (_items.Contains(item))
            {
                _items.Remove(item);
                Debug.Log($"Removed {item.itemName} from inventory.");
            }
        }

        /// <summary>
        /// Checks if the inventory contains a specific item.
        /// </summary>
        /// <param name="item">The item data to check for.</param>
        /// <returns>True if the item is in the inventory, false otherwise.</returns>
        public bool HasItem(Gameplay.ItemData item)
        {
            return _items.Contains(item);
        }
    }
}