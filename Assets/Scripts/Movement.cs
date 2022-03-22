using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustScale = 100f;
    [SerializeField] float rotationScale = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;
 
    Rigidbody rididPizza;
    AudioSource audioSource;

    void Start()
    {
        rididPizza = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopSpawnThrustingParticles();
        }
    }

    private void StopSpawnThrustingParticles()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    private void StartThrusting()
    {
        rididPizza.AddRelativeForce(Vector3.up * thrustScale * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    private void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            LeftRotation();
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RightRotation();
        }
        else
        {
            StopSpawnRotationParticles();
        }
    }

    private void StopSpawnRotationParticles()
    {
        leftBooster.Stop();
        rightBooster.Stop();
    }

    private void RightRotation()
    {
        ApplyRotation(-rotationScale);
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
    }

    private void LeftRotation()
    {
        ApplyRotation(rotationScale);
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rididPizza.freezeRotation = true; // фризим для получения норм контроля над вращением
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rididPizza.freezeRotation = false; //стоп фриз
    }
}
