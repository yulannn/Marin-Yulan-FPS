using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivityX = 500f;
    [SerializeField] private float mouseSensitivityY = 500f;
    [SerializeField] private float minYRotation = -60f;
    [SerializeField] private float maxYRotation = 60f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    [SerializeField] private float moveSpeed = 5f;

    private Transform playerBody;

    private void Start()
    {
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        rotationY += Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, minYRotation, maxYRotation);
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        MovePlayer();
    }

    private void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * moveZ + transform.right * moveX;
        playerBody.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
