using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugKeys : MonoBehaviour
{

    Collider cr;

    bool pushed;

    void Start()
    {
        cr = GetComponent<Collider>();
    }

    void Update()
    {
        CancleCollision();
    }

    void CancleCollision()//WIP
    {
        pushed = false;
        if(Input.GetKey(KeyCode.C) && !pushed)
        {
            cr.enabled=false;
            pushed = true;
            Debug.Log("Collision disacivated");
        }
        else
        {
            cr.enabled=true;
            pushed = false;
            Debug.Log("Collision acivated");
            
        }
    }
}
