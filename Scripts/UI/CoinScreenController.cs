using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScreenController : MonoBehaviour
{
    [SerializeField] private UICoinController coinPrefab;
    [SerializeField] private Transform moneyCounterTransform;
    [SerializeField] private Camera uiCamera;
    [SerializeField] private Camera mainCamera;
    private Vector3 _endPos;
    private void Awake()
    {
        EventManager.AddListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);

    }

    private void OnGameStart(GameStartEvent obj)
    {
        _endPos = moneyCounterTransform.localPosition;
    }

    private void OnMoneyCollect(MoneyCollectEvent obj)
    {
        Vector3 vp = mainCamera.WorldToScreenPoint(obj.PlayerPosition);
        Vector3 playerPosOnScreen = uiCamera.ScreenToWorldPoint(vp);
        UICoinController go = Instantiate(coinPrefab, transform);
        go.transform.position = playerPosOnScreen;
        go.StartFlight(_endPos);
    }
}
