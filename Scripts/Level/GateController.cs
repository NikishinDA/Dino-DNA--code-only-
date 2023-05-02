using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private int changeValue;

    public void BroadcastGateEvent()
    {
        var evt = GameEventsHandler.GateEvent;
        evt.ChangeValue = changeValue;
        EventManager.Broadcast(evt);
    }
}
