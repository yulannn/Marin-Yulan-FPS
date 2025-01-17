using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float fireRate = 0.2f;

    private float nextFireTime = 10f;
    private IA_Player myInputActions;

    private void Awake()
    {
        myInputActions = new IA_Player();
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


    GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

  
    Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
    if (bulletRb != null)
    {
        bulletRb.AddForce(firePoint.forward * 20f, ForceMode.VelocityChange);
    }
}


}
