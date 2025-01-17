using UnityEngine;
    using UnityEngine.InputSystem;
    public class Player_Controller : MonoBehaviour
    {

        [SerializeField]private int speed = 3;
        [SerializeField]private float jumpforce = 5;



        private IA_Player myInputActions;
        private InputAction moveAction;
        private InputAction jumpAction;
        private Rigidbody rb;



        void Awake() {
            myInputActions =  new IA_Player();
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable() {
            moveAction = myInputActions.Movements.Move;
            moveAction.Enable();
            jumpAction = myInputActions.Movements.Jump;
            jumpAction.performed += OnJump;
            jumpAction.Enable();
        }
        private void OnDisable() {
            moveAction.Disable();
            myInputActions.Movements.Jump.Disable();
        }

        void Start()
        {

        }

        private void OnJump(InputAction.CallbackContext callbackContext) {
            rb.AddForce(UnityEngine.Vector3.up * jumpforce, ForceMode.Impulse);
        }

        void FixedUpdate() {
            UnityEngine.Vector2 moveDir = moveAction.ReadValue<UnityEngine.Vector2>(); 
            UnityEngine.Vector3 vel =  rb.linearVelocity;
            vel.x = moveDir.x * speed; 
            vel.z = moveDir.y * speed;
            rb.linearVelocity = vel;
             
        }


    }