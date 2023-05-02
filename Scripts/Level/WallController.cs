using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : ItemController
{
    [SerializeField] private WallEffectController effectController;
    protected override void DetectionOnPlayerDetected(Transform playerTransform)
    {
        EventManager.Broadcast(GameEventsHandler.PlayerObstacleHitEvent);
        effectController.PlayEffect(playerTransform.position);
    }
}
