using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float gravity = -9.81f;
    public Transform cameraTransform; // Assign the Main Camera here
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private Vector3 velocity;
    private float rotationY = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Hide and lock cursor
    }

    void Update()
    {
        RotateWithMouse();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * z + transform.right * x;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void RotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        // Rotate player left/right
        rotationY += mouseX;
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        // Rotate camera with player (optional tilt/zoom can go here)
        if (cameraTransform != null)
        {
            cameraTransform.position = transform.position + Quaternion.Euler(0f, rotationY, 0f) * new Vector3(0, 2, -4);
            cameraTransform.LookAt(transform.position + Vector3.up * 1.5f);
        }
    }
}
