using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkAmbianceController : MonoBehaviour
{
    [SerializeField] private Material[] firstMaterials;
    [SerializeField] private Material[] secondMaterials;
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
        Material[] newMats = new Material[2];
        newMats[0] = firstMaterials[num];
        newMats[1] = secondMaterials[num];
        _renderer.materials = newMats;
    }
}
