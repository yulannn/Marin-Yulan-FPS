using UnityEngine;
using UnityEngine.SceneManagement; 

public class EndGame : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
           
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
