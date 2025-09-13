using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [Header("Cài đặt")]
    [SerializeField] private bool enableHeadBob = true;
    [SerializeField, Range(0.01f, 0.1f)] private float amplitude = 0.05f;
    [SerializeField, Range(5, 15)] private float frequency = 10.0f;

    [Header("Gán đối tượng")]
    [SerializeField] private Transform cameraTransform = null;
    
    // --- BIẾN ĐÃ THAY ĐỔI ---
    private CharacterController characterController;
    private PlayerMovement playerMovement; // Tham chiếu đến script PlayerMovement
    // -------------------------

    private Vector3 startLocalPos;

    private void Start()
    {
        // Lấy các component cần thiết từ chính đối tượng Player
        characterController = GetComponent<CharacterController>();
        playerMovement = GetComponent<PlayerMovement>();

        if (cameraTransform == null)
            Debug.LogError("LỖI: Chưa gán Camera Transform!");
        if (playerMovement == null)
            Debug.LogError("LỖI: Không tìm thấy script PlayerMovement trên Player!");

        startLocalPos = cameraTransform.localPosition;
    }

    private void Update()
    {
        if (!enableHeadBob || Time.timeScale == 0) return;

        // --- LOGIC ĐÃ THAY ĐỔI ---
        // Thay vì đọc velocity, chúng ta đọc trực tiếp input từ PlayerMovement
        float moveInputMagnitude = playerMovement.MoveInput.magnitude;
        // -------------------------

        Vector3 targetPosition;

        // Nếu người chơi đang nhấn phím di chuyển và đang ở trên mặt đất
        if (moveInputMagnitude > 0.1f && characterController.isGrounded)
        {
            // Tính toán và áp dụng hiệu ứng head bob
            targetPosition = startLocalPos + (CalculateHeadBobOffset(Time.time) * amplitude);
        }
        else
        {
            // Trở về vị trí ban đầu
            targetPosition = startLocalPos;
        }

        // Áp dụng vị trí mới cho camera một cách mượt mà
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetPosition, Time.deltaTime * 15f);
    }

    private Vector3 CalculateHeadBobOffset(float time)
    {
        float bobAmountY = Mathf.Sin(time * frequency);
        float bobAmountX = Mathf.Cos(time * frequency * 0.5f) * 0.5f;
        return new Vector3(bobAmountX, bobAmountY, 0);
    }
}