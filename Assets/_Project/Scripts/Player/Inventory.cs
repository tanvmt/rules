using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System.Text;
using System;

namespace NenNhangSinhMenh.Player
{
    public class Inventory : MonoBehaviour
    {
        private List<Gameplay.ItemData> _items = new List<Gameplay.ItemData>();

        void Start()
        {
            UpdateInventoryUI();
        }

        public void AddItem(Gameplay.ItemData item)
        {
            _items.Add(item);
            Debug.Log($"Added {item.itemName} to inventory.");
            UpdateInventoryUI();
        }

        private void UpdateInventoryUI()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Túi đồ:");
            if (_items.Count == 0)
            {
                sb.AppendLine(" - Trống - ");
            }
            else
            {
                foreach (var item in _items)
                {
                    sb.AppendLine($"- {item.itemName}");
                }
            }
            UI.UIManager.Instance.UpdateInventory(sb.ToString());
        }

        public void RemoveItem(Gameplay.ItemData item)
        {
            if (_items.Contains(item))
            {
                _items.Remove(item);
                Debug.Log($"Removed {item.itemName} from inventory.");
                UpdateInventoryUI();
            }
        }

        public bool HasItem(Gameplay.ItemData item)
        {
            return _items.Contains(item);
        }
    }
}