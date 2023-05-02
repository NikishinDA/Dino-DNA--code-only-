using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICoinController : MonoBehaviour
{
    public void StartFlight(Vector3 endPos)
    {
        StartCoroutine(flyCor(0.5f, transform.localPosition, endPos));
    }

    private IEnumerator flyCor(float time, Vector3 startPos, Vector3 endPos)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            transform.localPosition = Vector3.Lerp(startPos, endPos, (t * t) / (time * time));
            yield return null;
        }
        EventManager.Broadcast(GameEventsHandler.CoinUiCollectEvent);
        Destroy(gameObject);
    }
}
