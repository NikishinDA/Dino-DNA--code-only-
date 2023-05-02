using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrapController : ItemController
{
    [SerializeField] private BearTrapEffectController effectController;
    protected override void DetectionOnPlayerDetected(Transform playerTransform)
    {
        EventManager.Broadcast(GameEventsHandler.PlayerObstacleHitEvent);
        effectController.PlayEffect();
    }
}