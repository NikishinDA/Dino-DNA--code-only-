using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMeterManager : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private float speed = 1f;
    private float _progress;
    private bool _isFirst = true;
    private void Awake()
    {
        EventManager.AddListener<PlayerScoreChangeEvent>(OnPlayerScoreChange);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerScoreChangeEvent>(OnPlayerScoreChange);
    }

    private void OnPlayerScoreChange(PlayerScoreChangeEvent obj)
    {
        
        _progress = obj.Value / 100f;
        VarSaver.PlayerProgress = _progress;
        if (_isFirst)
        {
            fill.fillAmount = _progress;
            _isFirst = false;
        }
    }

    private void Update()
    {
        fill.fillAmount = Mathf.Lerp(fill.fillAmount, _progress, speed *  Time.deltaTime);
    }
}
