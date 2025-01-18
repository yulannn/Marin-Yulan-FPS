using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player_Controller : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;
    
    private IA_Player myInputActions;
    private InputAction moveAction;
    private InputAction crouchAction;
    private InputAction sprintAction;
    
    private bool isCrouching = false;
    private bool isSprinting = false;

    

    void Awake()
    {
  
        myInputActions = new IA_Player();
        characterController = GetComponent<CharacterController>();
        
    }

    private void OnEnable()
    {
      
        myInputActions.Enable();
        
      
        moveAction = myInputActions.Movements.Move;
        crouchAction = myInputActions.Movements.Crouch;
        sprintAction = myInputActions.Movements.Sprint;
    }

    private void OnDisable()
    {
      
        myInputActions.Disable();
    }

    void FixedUpdate()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

       
        isSprinting = sprintAction.ReadValue<float>() > 0.5f; 
        isCrouching = crouchAction.ReadValue<float>() > 0.5f;

     
        float curSpeedX = canMove ? (isSprinting ? runSpeed : walkSpeed) * moveAction.ReadValue<Vector2>().y : 0;
        float curSpeedY = canMove ? (isSprinting ? runSpeed : walkSpeed) * moveAction.ReadValue<Vector2>().x : 0;
        
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

       
        if (isCrouching && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
