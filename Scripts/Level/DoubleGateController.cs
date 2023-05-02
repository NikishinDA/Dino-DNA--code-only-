using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleGateController : MonoBehaviour
{
    [SerializeField]
    private GateController leftGate;

    [SerializeField] private GateController rightGate;
    [SerializeField] private GateDetection detection;

    private void Awake()
    {
        detection.PlayerDetected += DetectionOnPlayerDetected;
    }

    private void DetectionOnPlayerDetected(bool isRight)
    {
        if (isRight)
        {
            rightGate.BroadcastGateEvent();
        }
        else
        {
            leftGate.BroadcastGateEvent();
            
        }
    }
}
