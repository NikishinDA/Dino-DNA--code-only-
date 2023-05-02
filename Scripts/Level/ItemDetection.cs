using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetection : MonoBehaviour
{
    public event Action<Transform> PlayerDetected;
    private Collider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
        gameObject.layer = LayerMask.NameToLayer("Interaction");
        _trigger.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerDetected?.Invoke(other.transform.parent);
        _trigger.enabled = false;
    }
}
