using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : ItemController
{
    protected override void DetectionOnPlayerDetected(Transform playerTransform)
    {
        var evt = GameEventsHandler.MoneyCollectEvent;
        evt.PlayerPosition = playerTransform.position;
        EventManager.Broadcast(evt);
        base.DetectionOnPlayerDetected(playerTransform);
    }
}
