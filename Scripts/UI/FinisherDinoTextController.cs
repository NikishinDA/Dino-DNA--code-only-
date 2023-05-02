using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinisherDinoTextController : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    [SerializeField] private Text dinoText;
    [SerializeField] private string[] dinoNames;
    private void Awake()
    {
        EventManager.AddListener<FinisherPlayerProgressInfoEvent>(OnPlayerProgressInfo);
        
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<FinisherPlayerProgressInfoEvent>(OnPlayerProgressInfo);

    }

    private void OnPlayerProgressInfo(FinisherPlayerProgressInfoEvent obj)
    {
        int skinNum = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.SkinNumber);
        textObject.SetActive(true);
        dinoText.text = dinoNames[skinNum % dinoNames.Length].ToUpper();
    }
}
