using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFarDetection : MonoBehaviour
{
    public event Action<Transform> PlayerSpotted;
    public event Action PlayerStaysInTrigger;
    private void OnTriggerEnter(Collider other)
    {
        PlayerSpotted?.Invoke(other.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerStaysInTrigger?.Invoke();
    }
}
