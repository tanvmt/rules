using UnityEngine;
using UnityEngine.InputSystem;

namespace NenNhangSinhMenh
{
    public class MouseLook : MonoBehaviour
    {
        [Header("Mouse Settings")]
        [SerializeField] private float mouseSensitivity = 100.0f;
        [SerializeField] private Transform playerBody;

        private PlayerControls _playerControls;
        private float _xRotation = 0f;

        private void Awake()
        {
            _playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            _playerControls.Player.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Player.Disable();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            HandleMouseLook();
        }

        private void HandleMouseLook()
        {
            Vector2 lookInput = _playerControls.Player.Look.ReadValue<Vector2>();

            float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
            float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}