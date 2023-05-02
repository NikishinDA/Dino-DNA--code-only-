using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
    [SerializeField] private InputField[] inputs;
    [SerializeField] private Button confirmButton;
    private Dictionary<int, int> _intValues;
    private Dictionary<int, float> _floatValues;

    private void Awake()
    {
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        _intValues = new Dictionary<int, int>();
        _floatValues = new Dictionary<int, float>();
        for (var i = 0; i < inputs.Length; i++)
        {
            switch (inputs[i].characterValidation)
            {
                case InputField.CharacterValidation.Integer:
                {
                    int value = PlayerPrefs.GetInt("DebugValue" + i, -1);
                    _intValues.Add(i, value);
                    if (value > -1)
                    {
                        inputs[i].text = value.ToString();
                    }
                }
                    break;
                case InputField.CharacterValidation.Decimal:
                {
                    float value = PlayerPrefs.GetFloat("DebugValue" + i, -1);
                    _floatValues.Add(i, value);
                    if (value > -1)
                    {
                        inputs[i].text = value.ToString();
                    }
                }
                    break;
                default:
                    throw new Exception("Wrong debug value type.");
            }
        }
    }

    private void OnConfirmButtonClick()
    {
        for (var i = 0; i < inputs.Length; i++)
        {
            switch (inputs[i].characterValidation)
            {
                case InputField.CharacterValidation.Integer:
                {
                    int.TryParse(inputs[i].text, out var value);
                    _intValues[i] = value;
                }
                    break;
                case InputField.CharacterValidation.Decimal:
                {
                    float.TryParse(inputs[i].text, out var value);
                    _floatValues[i] = value;
                }
                    break;
            }
        }

        var evt = GameEventsHandler.DebugCallEvent;
        evt.IntValues = _intValues;
        evt.FloatValues = _floatValues;
        EventManager.Broadcast(evt);

        foreach (var value in _intValues)
        {
            PlayerPrefs.SetInt("DebugValue" + value.Key, value.Value);
        }

        foreach (var value in _floatValues)
        {
            PlayerPrefs.SetFloat("DebugValue" + value.Key, value.Value);
        }

        PlayerPrefs.Save();
    }
}