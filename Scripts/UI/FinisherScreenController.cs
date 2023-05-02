using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherScreenController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var evt = GameEventsHandler.GameOverEvent;
            evt.IsWin = true;
            EventManager.Broadcast(evt);
        }
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        var evt = GameEventsHandler.GameOverEvent;
        evt.IsWin = true;
        EventManager.Broadcast(evt);
    }
}
