using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDetection : MonoBehaviour
{
    public event Action<bool> PlayerDetected;
    private Collider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
        gameObject.layer = LayerMask.NameToLayer("Interaction");
        _trigger.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        float deltaX = transform.position.x - other.transform.position.x;
        bool isRight = deltaX < 0;
        PlayerDetected?.Invoke(isRight);
        _trigger.enabled = false;
    }
}
