using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspensionWManager : MonoBehaviour
{
    public SuspensionW[] suspensionWs;
    
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
    }

    void FixedUpdate()
    {

    }
}
