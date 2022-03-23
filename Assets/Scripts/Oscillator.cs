using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movemenVector;
    [SerializeField] float period = 2f;
    [SerializeField] float delayTime = 1f;
    float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("StartMovements",delayTime);
    }

    private void StartMovements()
    {
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movemenVector * movementFactor;
        transform.position = offset + startingPos;
    }
}
