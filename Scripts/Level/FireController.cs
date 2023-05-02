using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : ItemController
{
    protected override void DetectionOnPlayerDetected(Transform playerTransform)
    {
        EventManager.Broadcast(GameEventsHandler.PlayerFireHitEvent);
    }
}
