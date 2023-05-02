using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugToggleButton : MonoBehaviour
{
    [SerializeField] private Button toggleButton;
    [SerializeField] private GameObject debugWindow;

    private bool _isActive;
    private void Awake()
    {
        toggleButton.onClick.AddListener(OnToggleButtonClick);
    }

    private void OnToggleButtonClick()
    {
        _isActive = !_isActive;
        debugWindow.SetActive(_isActive);
    }
}
