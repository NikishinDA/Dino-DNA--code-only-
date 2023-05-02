using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : ItemController
{
    [SerializeField] private SawMoveController moveController;
    [SerializeField] private SawEffectController effectController;

    protected override void Awake()
    {
        base.Awake();
        moveController.SawDirectionChange += MoveControllerOnSawDirectionChange;
    }

    private void MoveControllerOnSawDirectionChange(int obj)
    {
        if (obj < 0)
        {
            effectController.SpinLeft();
        }
        else
        {
            effectController.SpinRight();
        }
    }

    protected override void DetectionOnPlayerDetected(Transform playerTransform)
    {
        EventManager.Broadcast(GameEventsHandler.PlayerObstacleHitEvent);
    }
}
