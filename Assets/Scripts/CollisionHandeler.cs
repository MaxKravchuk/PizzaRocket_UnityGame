using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandeler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Launch pad":
                Debug.Log("You are on the start");
                break;
            case "Finish":
                LoadNexLevel();
                break;
            case "Fuel":
                Debug.Log("Fuel renewed");
                break;
            case "Friendly":
                Debug.Log("Oh! Hello!");
                break;            
            default:
                ReloadLevel();
                break;

        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentSceneIndex);
        Debug.Log("Oops, you are blew up!");
    }

    void LoadNexLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextLevelIndex<=SceneManager.sceneCount)
        {
            SceneManager.LoadScene(nextLevelIndex);
            Debug.Log($"This is {nextLevelIndex} level");
        }
        else
        {
            SceneManager.LoadScene(0);
            Debug.Log($"This is {nextLevelIndex} level");
        }
    }
}
