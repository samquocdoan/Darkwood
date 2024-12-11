using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5f; // Tốc độ di chuyển
    public Transform cameraTransform; // Camera để lấy hướng di chuyển

    private void Update()
    {
        // Nhận input thô (không mượt)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Hướng di chuyển theo camera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Animation
        bool isMoving = horizontal != 0 || vertical != 0;
        animator.SetBool("isWalking", isMoving);

        // Chỉ di chuyển trên mặt phẳng (bỏ Y)
        forward.y = 0f;
        right.y = 0f;

        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;

        // Kiểm tra có di chuyển hay không
        if (moveDirection.magnitude > 0.1f)
        {
            // Di chuyển
            Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);

            // Quay mặt nhân vật theo hướng di chuyển
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }
}
