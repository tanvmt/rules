using UnityEngine;

namespace NenNhangSinhMenh.Interactables
{
    public class PickupItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private Gameplay.ItemData itemData;

        private bool _isInteracted = false;

        public string InteractionPrompt => $"Nhặt {itemData.itemName}";

        public bool Interact()
        {
            if (_isInteracted) return false;
            _isInteracted = true;

            Player.Inventory playerInventory = FindFirstObjectByType<Player.Inventory>();
            if (playerInventory != null)
            {
                playerInventory.AddItem(itemData);
                Destroy(gameObject);
                return true;
            }
            return false;
        }
    }
}