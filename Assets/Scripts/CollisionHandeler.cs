using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandeler : MonoBehaviour
{
    [SerializeField] float delayRespawnTime = 1f;
    [SerializeField] float delayLoadLevelTime = 1f;
    [SerializeField] AudioClip successAudio;
    [SerializeField] AudioClip crachAudio;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Launch pad":
                Debug.Log("You are on the start");
                break;
            case "Finish":
                StartSequence("LoadNextLevel", successAudio, delayLoadLevelTime);
                break;
            case "Fuel":
                Debug.Log("Fuel renewed");
                break;
            case "Friendly":
                Debug.Log("Oh! Hello!");
                break;            
            default:
                StartSequence("ReloadLevel", crachAudio, delayRespawnTime);
                break;

        }
    }

    void StartSequence(string methodName, AudioClip audioName, float delayTime)
    {
        audioSource.PlayOneShot(audioName);
        GetComponent<Movement>().enabled = false;
        Invoke(methodName,delayTime);
    }

    // void StartCrashSequence(float delayTime)
    // {
    //     GetComponent<Movement>().enabled=false;
    //     Invoke("ReloadLevel",delayTime);   
    // }

    // void StartLoadLevelSequence(float delayTime)
    // {
    //     GetComponent<Movement>().enabled=false;
    //     Invoke("LoadNextLevel",delayTime);
    // }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        Debug.Log("Oops, you are blew up!");
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(nextLevelIndex >= SceneManager.sceneCountInBuildSettings)
            nextLevelIndex = 0;
        
        Debug.Log($"This is {nextLevelIndex} level");
        SceneManager.LoadScene(nextLevelIndex);
    }
}
