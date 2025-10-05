using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using NenNhangSinhMenh.Interactables;

namespace NenNhangSinhMenh.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Interaction Settings")]
        [SerializeField] private float interactionDistance = 3.0f;
        [SerializeField] private Transform cameraTransform;

        private PlayerControls _playerControls;

        private void Awake()
        {
            _playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            _playerControls.Player.Enable();
            _playerControls.Player.Interact.performed += OnInteract;
        }

        private void OnDisable()
        {
            _playerControls.Player.Disable();
            _playerControls.Player.Interact.performed -= OnInteract;
        }

        private void Update()
        {
            CheckForInteractable();
        }

        private void CheckForInteractable()
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    UI.UIManager.Instance.UpdateInteractionPrompt(interactable.InteractionPrompt);
                    return;
                }
            }

            UI.UIManager.Instance.UpdateInteractionPrompt("");
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}