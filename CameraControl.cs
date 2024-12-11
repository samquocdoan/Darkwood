using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player; // Nhân vật trung tâm
    public float distance = 5f; // Khoảng cách từ camera đến nhân vật
    public float height = 2f; // Độ cao camera
    public float sensitivity = 100f; // Độ nhạy chuột

    private float yaw = 0f; // Góc xoay ngang (trục Y)
    private float pitch = 0f; // Góc xoay dọc (trục X)

    private void Start()
    {
        // Ẩn và khóa chuột
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        // Lấy chuyển động chuột
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Tính toán góc xoay
        yaw += mouseX; // Xoay ngang
        pitch -= mouseY; // Xoay dọc
        pitch = Mathf.Clamp(pitch, -30f, 60f); // Giới hạn góc dọc (tránh camera lật ngược)

        // Tính vị trí mới của camera
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = new Vector3(0, height, -distance);
        transform.position = player.position + rotation * offset;

        // Camera nhìn vào nhân vật
        transform.LookAt(player.position + Vector3.up * height / 2);
    }
}
