using UnityEngine;
using TMPro;

namespace NenNhangSinhMenh.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI interactionPromptText;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI inventoryText;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void UpdateInteractionPrompt(string prompt)
        {
            interactionPromptText.text = prompt;
            interactionPromptText.gameObject.SetActive(!string.IsNullOrEmpty(prompt));
        }
        public void UpdateTimer(string time)
        {
            timerText.text = time;
        }

        public void UpdateInventory(string items)
        {
            inventoryText.text = items;
        }
    }
}