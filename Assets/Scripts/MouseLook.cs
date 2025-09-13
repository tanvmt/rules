using UnityEngine;
using UnityEngine.InputSystem; // Thêm dòng này

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;

    private float xRotation = 0f;
    private PlayerControls playerControls; // Tham chiếu đến file Input Actions

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Đọc giá trị di chuyển chuột từ input action
        Vector2 lookInput = playerControls.Player.Look.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Tính toán góc quay lên/xuống (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Quay camera lên/xuống
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Quay toàn bộ cơ thể người chơi sang trái/phải
        playerBody.Rotate(Vector3.up * mouseX);
    }
}