using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 200;
    [SerializeField] private float fireRate = 0.05f;

    private float nextFireTime = 0f;
    private IA_Player myInputActions;
    private Collider playerCollider;

    private void Awake()
    {
        myInputActions = new IA_Player();
        playerCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        myInputActions.Enable();
        myInputActions.Movements.Shoot.performed += OnShoot;
    }

    private void OnDisable()
    {
        myInputActions.Movements.Shoot.performed -= OnShoot;
        myInputActions.Disable();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireRate;

      
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

      
        bulletRb.useGravity = false;
        bulletRb.isKinematic = true;

       
        Collider bulletCollider = bullet.GetComponent<Collider>();
        if (bulletCollider != null && playerCollider != null)
        {
            Physics.IgnoreCollision(bulletCollider, playerCollider);
        }

      
        Vector3 shootDirection = firePoint.forward;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            shootDirection = (hit.point - firePoint.position).normalized;
        }

      
        bulletRb.isKinematic = false; 
        bulletRb.AddForce(shootDirection * bulletSpeed, ForceMode.VelocityChange);


        Destroy(bullet, 3f);
    }
}
