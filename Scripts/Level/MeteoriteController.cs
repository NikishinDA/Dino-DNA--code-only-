using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteController : ItemController
{
    [SerializeField] private MeteoriteEffectController effectController;
    protected override void DetectionOnPlayerDetected(Transform playerTransform)
    {
        effectController.Play();
        EventManager.Broadcast(GameEventsHandler.PlayerObstacleHitEvent);
    }
}
