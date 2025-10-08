using UnityEngine;
using UnityEngine.InputSystem;

namespace NenNhangSinhMenh
{
    public class LighterController : MonoBehaviour
    {
        [SerializeField] private Light lighterLight;
        [SerializeField] private Gameplay.ItemData lighterItemData;

        private Player.Inventory _playerInventory;
        private PlayerControls _playerControls;
        private bool _isLighterOn = false;

        private void Awake()
        {
            _playerInventory = GetComponentInParent<Player.Inventory>();
            _playerControls = new PlayerControls();
            lighterLight.enabled = false;
        }

        private void OnEnable()
        {
            _playerControls.Player.Enable();
            _playerControls.Player.ToggleLighter.performed += OnToggleLighter;
        }

        private void OnDisable()
        {
            _playerControls.Player.Disable();
            _playerControls.Player.ToggleLighter.performed -= OnToggleLighter;
        }

        private void OnToggleLighter(InputAction.CallbackContext context)
        {
            if(_playerInventory != null && _playerInventory.HasItem(lighterItemData))
            {
                _isLighterOn = !_isLighterOn;
                lighterLight.enabled = _isLighterOn;
            }
        }
    }
}
