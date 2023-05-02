using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLaneController : ItemController
{
    protected override void DetectionOnPlayerDetected(Transform playerTransform)
    {
        var evt = GameEventsHandler.PlayerFinishReachedEvent;
        evt.PlayerTransform = playerTransform;
        EventManager.Broadcast(evt);
    }
}

