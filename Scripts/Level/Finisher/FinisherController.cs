using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherController : MonoBehaviour
{
    private Transform _playerTransform;
    [SerializeField] private Transform playerHolder;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private GameObject finisherCamera;
    private void Awake()
    {
        EventManager.AddListener<PlayerFinishReachedEvent>(OnPlayerFinishReach);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerFinishReachedEvent>(OnPlayerFinishReach);
    }

    private void OnPlayerFinishReach(PlayerFinishReachedEvent obj)
    {
        _playerTransform = obj.PlayerTransform;
        _playerTransform.SetParent(playerHolder);
        StartCoroutine(PullPlayer(_playerTransform, 0.5F, 0.5F));
        finisherCamera.SetActive(true);
    }

    private IEnumerator PullPlayer(Transform playerTransform, float playerPositioningTime, float moveTime)
    {
        Vector3 startPos = playerTransform.localPosition;
        Vector3 endPos = Vector3.zero;

        for (float t = 0; t < playerPositioningTime; t += Time.deltaTime)
        {
            _playerTransform.localPosition = Vector3.Lerp(startPos, endPos, t / playerPositioningTime);
            yield return null;
        }

        _playerTransform.localPosition = endPos;

        Queue<Transform> waypointQueue = new Queue<Transform>(waypoints);

        while (waypointQueue.Count > 0)
        {
            startPos = playerHolder.position;
            endPos = waypointQueue.Dequeue().position;
            for (float t = 0; t < moveTime; t += Time.deltaTime)
            {
                playerHolder.position = Vector3.Lerp(startPos, endPos, t /moveTime);
                yield return null;
            }
        }
        EventManager.Broadcast(GameEventsHandler.FinisherPlayerInPosition);
    }
}
