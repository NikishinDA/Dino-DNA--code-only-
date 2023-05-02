using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowcaseAmbianceManager : MonoBehaviour
{
    [SerializeField] private GameObject[] ambiances;
    [SerializeField] private Material[] skyboxes;
    [SerializeField] private Transform ambianceTransform;

    private int _ambianceNum;

    private void Awake()
    {
        EventManager.AddListener<AmbianceInfoEvent>(OnAmbianceInfo);

        _ambianceNum = Random.Range(0, ambiances.Length);
    }

    private void Start()
    {
        Instantiate(ambiances[_ambianceNum], ambianceTransform);
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