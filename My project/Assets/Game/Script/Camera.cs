using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivityX = 2f;  
    [SerializeField] private float mouseSensitivityY = 2f; 
    [SerializeField] private float minYRotation = -90f;     
    [SerializeField] private float maxYRotation = 90f;      

    private float rotationX = 0f;
    private float rotationY = 0f;

    private Transform playerBody;

    private void Start()
    {
        playerBody = transform.parent; 
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false; 
    }

    private void Update()
    {
      
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;

        rotationY += mouseX;

       
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minYRotation, maxYRotation);

    
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

      
        playerBody.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }
}
