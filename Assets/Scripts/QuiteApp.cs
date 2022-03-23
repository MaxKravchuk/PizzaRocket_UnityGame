using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuiteApp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quitted");
            Application.Quit();
        }        
    }
}
