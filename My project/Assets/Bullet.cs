using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Afficher un message si la balle touche quelque chose
        Debug.Log("La balle a touché: " + collision.gameObject.name);

        // Si la balle touche un objet avec le tag "Window"
        if (collision.gameObject.CompareTag("Window"))
        {
            // Détruire la fenêtre
            Destroy(collision.gameObject);

            // Détruire la balle
            Destroy(gameObject);
        }
    }
}
