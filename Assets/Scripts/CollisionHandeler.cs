using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandeler : MonoBehaviour
{
    [SerializeField] float delayRespawnTime = 1f;
    [SerializeField] float delayLoadLevelTime = 1f;
    [SerializeField] AudioClip successAudio;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys(); 
    }

    private void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable; //toggle collision
        }
    }

    void OnCollisionEnter(Collision other)
    {
        ObstacleCollisionDetection(other);
    }

    private void ObstacleCollisionDetection(Collision other)
    {
        if(collisionDisable)
            return;
        switch (other.gameObject.tag)
        {
            case "Launch pad":
                Debug.Log("You are on the start");
                break;
            case "Finish":
                StartSequence("LoadNextLevel", successAudio, delayLoadLevelTime, successParticles);
                break;
            case "Fuel":
                Debug.Log("Fuel renewed");
                break;
            case "TheEnd":
                StartSequence("LoadTheEndLevel", successAudio, delayLoadLevelTime, successParticles);
                break;
            default:
                StartSequence("ReloadLevel", crashAudio, delayRespawnTime, crashParticles);
                break;

        }
    }

    void StartSequence(string methodName, AudioClip audioName, float delayTime, ParticleSystem particles)
    {
        GetComponent<Movement>().enabled = false;
        if(!isTransitioning && audioName!=null && particles!=null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(audioName);
            particles.Play();
            isTransitioning = true;
        }
        Invoke(methodName,delayTime);
    }

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

    void LoadTheEndLevel()
    {
        SceneManager.LoadScene("The End");
    }
}
