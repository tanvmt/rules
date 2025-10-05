using UnityEngine;

namespace NenNhangSinhMenh.Interactables
{
    public class InteractiveDoor : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _prompt = "Mở Cửa";
        private bool _isOpen = false;
        private Animator _animator;

        public string InteractionPrompt => _prompt;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public bool Interact()
        {
            _isOpen = !_isOpen;

            if (_animator != null)
            {
                 _animator.SetBool("IsOpen", _isOpen);
            }
            
            Debug.Log(_isOpen ? "Door opened!" : "Door closed!");

            _prompt = _isOpen ? "Đóng Cửa" : "Mở Cửa";

            return true;
        }
    }
}