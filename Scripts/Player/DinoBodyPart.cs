using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DinoBodyPart : MonoBehaviour
{
    [SerializeField] private bool hideRest;

    public event Action Hide;
    public event Action Show;
    public bool HideRest => hideRest;

    public abstract void SetActive(bool isActive);

    public abstract void SetActiveImmediate(bool isActive);

    protected void BroadcastHideEvent()
    {
        Hide?.Invoke();
    }

    protected void BroadcastShowEvent()
    {
        Show?.Invoke();
    }
}
