using UnityEngine;


public class DNAController : ItemController
{
    protected override void DetectionOnPlayerDetected(Transform playerTransform)
    {
        var evt = GameEventsHandler.PlayerDNACollectEvent;
        evt.PlayerPosition = playerTransform.position;
        EventManager.Broadcast(evt);
        base.DetectionOnPlayerDetected(playerTransform);
    }
}