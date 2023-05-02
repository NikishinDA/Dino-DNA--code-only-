using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceManager : MonoBehaviour
{
    [SerializeField] private int ambiancePerLevel;
    [SerializeField] private GameObject[] ambiances;
    [SerializeField] private Material[] skyboxes;
    
    private int _ambianceNum;
    private void Awake()
    {
        EventManager.AddListener<AmbianceInfoEvent>(OnAmbianceInfo);

        int level = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.Level) - 1;
        _ambianceNum = (level / ambiancePerLevel) % ambiances.Length;
    }

    private void Start()
    {
        Instantiate(ambiances[_ambianceNum]);
        RenderSettings.skybox = skyboxes[_ambianceNum];
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<AmbianceInfoEvent>(OnAmbianceInfo);

    }

    private void OnAmbianceInfo(AmbianceInfoEvent obj)
    {
        obj.Callback.Invoke(_ambianceNum);
    }
}
