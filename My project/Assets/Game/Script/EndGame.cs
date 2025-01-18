using UnityEngine;
using UnityEngine.SceneManagement;  
public class EndGame : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "SceneName"; 

   
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("EndPlatform"))
        {
          
            RestartGame();
        }
    }

      private void RestartGame()
    {
  
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
