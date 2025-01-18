using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float fireRate = 0.2f;

    private float nextFireTime = 0f;
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
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireRate;

        // Instancier la balle
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Raycast pour viser au centre de l'écran
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 direction = (hit.point - firePoint.position).normalized;
            bulletRb.linearVelocity = direction * bulletSpeed;
        }
        else
        {
            bulletRb.linearVelocity = firePoint.forward * bulletSpeed;
        }
    }
}
