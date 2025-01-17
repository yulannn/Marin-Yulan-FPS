using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 5f;

    private IA_Player myInputActions;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Rigidbody rb;

    void Awake()
    {
        myInputActions = new IA_Player();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        moveAction = myInputActions.Movements.Move;
        moveAction.Enable();

        jumpAction = myInputActions.Movements.Jump;
        jumpAction.performed += OnJump;
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        myInputActions.Movements.Jump.Disable();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        Vector2 moveDir = moveAction.ReadValue<Vector2>();
        Vector3 velocity = rb.linearVelocity;

        velocity.x = moveDir.x * speed;
        velocity.z = moveDir.y * speed;

        rb.linearVelocity = velocity;
    }
}
