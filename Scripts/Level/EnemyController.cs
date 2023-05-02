using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyFarDetection farDetection;
    [SerializeField] private ItemDetection closeDetection;

    [SerializeField] private Animator animator;

    private Transform _playerTransform;

    private void Awake()
    {
        farDetection.PlayerSpotted += FarDetectionOnPlayerSpotted;
        farDetection.PlayerStaysInTrigger += FarDetectionOnPlayerStaysInTrigger;
        closeDetection.PlayerDetected += CloseDetectionOnPlayerDetected;
    }

    private void CloseDetectionOnPlayerDetected(Transform playerTransform)
    {
        animator.SetTrigger("Strike");
        EventManager.Broadcast(GameEventsHandler.PlayerObstacleHitEvent);
    }

    private void FarDetectionOnPlayerStaysInTrigger()
    {
        if (_playerTransform)
            transform.LookAt(_playerTransform);
    }

    private void FarDetectionOnPlayerSpotted(Transform obj)
    {
        animator.SetTrigger("Ready");
        _playerTransform = obj;
    }
}