using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspensionWManager : MonoBehaviour
{
    public SuspensionW[] suspensionWs;

    [Header("Properties")]
    public float springConstant = 100.0f;
    public float damping = 10.0f;
    public float distance = 0.0f;
    public float rayLength = 10.0f;
    public int numberOfWheels = 4;
    public float groundHeight = 0.0f;
    public float minHeightAboveGround = 0.1f;

    void Start()
    {
        // updateProperties();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    [ContextMenu("Update")]
    public void updateProperties() {
        foreach (SuspensionW suspensionW in suspensionWs) 
        {
            suspensionW.springConstant = springConstant;
            suspensionW.damping = damping;
            suspensionW.distance = distance;
            suspensionW.rayLength = rayLength;
            suspensionW.numberOfWheels = numberOfWheels;
            suspensionW.groundHeight = groundHeight;
            suspensionW.minHeightAboveGround = minHeightAboveGround;
        } 
    }
}
