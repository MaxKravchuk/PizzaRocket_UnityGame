using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustScale = 100f;
    [SerializeField] float rotationScale = 1f;
    [SerializeField] AudioClip mainEngine;
 
    Rigidbody rididPizza;
    AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        rididPizza = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        
        if(Input.GetKey(KeyCode.Space))
        {
            rididPizza.AddRelativeForce(Vector3.up * thrustScale * Time.deltaTime);
            
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(rotationScale);
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-rotationScale);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rididPizza.freezeRotation = true; // фризим для получения норм контроля над вращением
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rididPizza.freezeRotation = false; //стоп фриз
    }
}
