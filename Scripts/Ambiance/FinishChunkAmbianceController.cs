using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishChunkAmbianceController : MonoBehaviour
{
    [SerializeField] private Material[] firstAmbiance;
    [SerializeField] private Material[] secondAmbiance;
    [SerializeField] private Material[] thirdAmbiance;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        var evt = GameEventsHandler.AmbianceInfoEvent;
        evt.Callback = SetMaterials;
        EventManager.Broadcast(evt);
    }

    private void SetMaterials(int num)
    {
        switch (num)
        {
            case 0:
            {
                _renderer.materials = firstAmbiance;
            }
                break;
            case 1:
            {
                _renderer.materials = secondAmbiance;
            }
                break;
            case 2:
            {
                _renderer.materials = thirdAmbiance;
            }
                break;
            default:
            {
                throw new Exception("Wrong ambiance num");
            }
        }
    }
}