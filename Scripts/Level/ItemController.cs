using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private ItemDetection detection;

    protected virtual void Awake()
    {
        detection.PlayerDetected += DetectionOnPlayerDetected;
    }

    protected virtual void DetectionOnPlayerDetected(Transform playerTransform)
    {
        Destroy(gameObject);
    }
}
