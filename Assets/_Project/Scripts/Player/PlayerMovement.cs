using UnityEngine;
using UnityEngine.InputSystem;

namespace NenNhangSinhMenh
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5.0f;
        [SerializeField] private float gravity = -9.81f;

        private CharacterController _characterController;
        private PlayerControls _playerControls;
        private Vector3 _verticalVelocity;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
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

        private void Update()
        {
            HandleGravity();
            HandleMovement();
        }

        private void HandleGravity()
        {
            if (_characterController.isGrounded && _verticalVelocity.y < 0)
            {
                _verticalVelocity.y = -2f;
            }

            _verticalVelocity.y += gravity * Time.deltaTime;
            _characterController.Move(_verticalVelocity * Time.deltaTime);
        }

        private void HandleMovement()
        {
            Vector2 moveInput = _playerControls.Player.Move.ReadValue<Vector2>();
            Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
            _characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}