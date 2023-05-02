using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartsManager : MonoBehaviour
{
    [Header("Dino Parts")] [SerializeField]
    private DinoBodyPart[] bodyParts;

    [SerializeField] private MuscleBodyPart skeletonObject;
    [SerializeField] private GameObject[] misc;
    [SerializeField] private float[] scoresThresholds;
    private int _currentStage;
    private void Awake()
    {
        foreach (var bodyPart in bodyParts)
        {
            if (!bodyPart.HideRest) continue;
            bodyPart.Hide += OnHide;
            bodyPart.Show += OnShow;
        }

        EventManager.AddListener<PlayerScoreChangeEvent>(OnPlayerScoreChange);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerScoreChangeEvent>(OnPlayerScoreChange);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);

    }


    private void OnGameOver(GameOverEvent obj)
    {
        if (obj.IsWin) return;
        skeletonObject.SetActive(false);
        foreach (var part in misc)
        {
            part.SetActive(false);
        }
    }

    private void OnPlayerScoreChange(PlayerScoreChangeEvent obj)
    {
        if (obj.IsImmediate)
        {
            ActivateImmediate(obj.Stage);
        }
        else
        {
            ToggleBodyParts(obj.Stage);
        }
    }

    private void OnShow()
    {
        HideInsides();
    }

    private void OnHide()
    {
        ShowInsides();
    }

    public void ToggleBodyParts(int stage)
    {
        if (stage == _currentStage || stage > bodyParts.Length) return;
        if (stage > _currentStage)
        {
            _currentStage = stage;
            bodyParts[stage - 1].SetActive(true);
            if (!bodyParts[stage - 1].HideRest) return;
            //HideInsides();
        }
        else
        {
            _currentStage = stage;
            bodyParts[stage].SetActive(false);
            if (!bodyParts[stage].HideRest) return;
            //ShowInsides();
        }
    }

    public void ActivateImmediate(int stage)
    {
        for (int i = 1; i <= stage; i++)
        {
            bodyParts[i - 1].SetActiveImmediate(true);
        }
    }

    private void ShowInsides()
    {
        for (int i = 0; i < bodyParts.Length - 1; i++)
        {
            bodyParts[i].SetActiveImmediate(true);
        }

        skeletonObject.SetActiveImmediate(true);
    }

    private void HideInsides()
    {
        for (int i = 0; i < bodyParts.Length - 1; i++)
        {
            bodyParts[i].SetActiveImmediate(false);
        }

        skeletonObject.SetActiveImmediate(false);
    }


}