using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
       
        Debug.Log("La balle a touché: " + collision.gameObject.name);

   
        if (collision.gameObject.CompareTag("Window"))
        {
          
            Destroy(collision.gameObject);
            
        
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag("Mob"))
        {
          
            Destroy(collision.gameObject);
            
          
            Destroy(gameObject);
        }
    }
}
