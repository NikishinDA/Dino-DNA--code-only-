using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject startCamera;

    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
    }

    private void OnGameStart(GameStartEvent obj)
    {
     //   playerCamera.SetActive(true);
        startCamera.SetActive(false);
    }
}
