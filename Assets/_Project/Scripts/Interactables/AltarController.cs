using UnityEngine;

namespace NenNhangSinhMenh.Interactables
{
    public class AltarController : MonoBehaviour, IInteractable
    {
        [SerializeField] private Gameplay.ItemData requiredIncense;
        [SerializeField] private Gameplay.ItemData requiredLighter;
        
        private bool _isIncenseLit = false;

        public string InteractionPrompt
        {
            get
            {
                if (_isIncenseLit) return "";
                
                Player.Inventory playerInventory = FindFirstObjectByType<Player.Inventory>();
                if (playerInventory == null) return "Lỗi: Không tìm thấy túi đồ";

                bool hasIncense = playerInventory.HasItem(requiredIncense);
                bool hasLighter = playerInventory.HasItem(requiredLighter);

                if (hasIncense && hasLighter)
                {
                    return "Thắp Nhang";
                }
                else if (!hasIncense)
                {
                    return "Cần có nhang";
                }

                return "Cần có bật lửa";
            }
        }

        public bool Interact()
        {
            if (_isIncenseLit) return false;

            Player.Inventory playerInventory = FindFirstObjectByType<Player.Inventory>();
            if (playerInventory != null && playerInventory.HasItem(requiredIncense) && playerInventory.HasItem(requiredLighter))
            {
                _isIncenseLit = true;

                playerInventory.RemoveItem(requiredIncense);

                Core.TimeManager.Instance.StartTimer();

                Core.ItemSpawnerManager.Instance.SpawnNextIncense();
                
                Debug.Log("Đã thắp nhang lên bàn thờ. Cây nhang tiếp theo đã xuất hiện ở đâu đó.");
                return true;
            }
            
            Debug.Log("Không đủ vật phẩm để thắp!");
            return false;
        }
    }
}