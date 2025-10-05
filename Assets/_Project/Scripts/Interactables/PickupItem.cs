using UnityEngine;

namespace NenNhangSinhMenh.Interactables
{
    /// <summary>
    /// Allows an object in the world to be picked up by the player.
    /// </summary>
    public class PickupItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private Gameplay.ItemData itemData;

        // We use the item name from the ScriptableObject as the prompt.
        public string InteractionPrompt => $"Nhặt {itemData.itemName}";

        public bool Interact()
        {
            // Find the player's inventory and add the item.
            Player.Inventory playerInventory = FindFirstObjectByType<Player.Inventory>();
            if (playerInventory != null)
            {
                playerInventory.AddItem(itemData);
                Destroy(gameObject); // The object disappears after being picked up.
                return true;
            }
            return false;
        }
    }
}