using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowcaseUIController : MonoBehaviour
{
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private Text dinoNameText;
    private void Awake()
    {
        leftButton.onClick.AddListener(OnLeftButtonClick);
        rightButton.onClick.AddListener(OnRightButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
        selectButton.onClick.AddListener(OnSelectButtonClick);
        EventManager.AddListener<ShowcaseSkinInfoEvent>(OnSkinInfo);
        EventManager.AddListener<ShowcaseSkinShowEvent>(OnSkinShow);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<ShowcaseSkinInfoEvent>(OnSkinInfo);
        EventManager.RemoveListener<ShowcaseSkinShowEvent>(OnSkinShow);

    }

    private void OnSkinShow(ShowcaseSkinShowEvent obj)
    {
        ShowName(obj.Name);
    }

    private void OnSkinInfo(ShowcaseSkinInfoEvent obj)
    {
        if (!obj.AllowSelect)
        {
            selectButton.interactable = false;
        }
        else
        {
            selectButton.onClick.AddListener(OnSelectButtonClick);
        }

    }

    private void OnSelectButtonClick()
    {
        EventManager.Broadcast(GameEventsHandler.ShowcaseSelectEvent);
        SceneManager.LoadScene(0);
    }

    private void OnExitButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    private void OnRightButtonClick()
    {
        var evt = GameEventsHandler.ShowcaseSwitchEvent;
        evt.IsNext = true;
        evt.Callback = ShowName;
        EventManager.Broadcast(evt);
    }

    private void OnLeftButtonClick()
    {
        var evt = GameEventsHandler.ShowcaseSwitchEvent;
        evt.IsNext = false;
        evt.Callback = ShowName;
        EventManager.Broadcast(evt);
    }

    private void ShowName(string dinoName)
    {
        dinoNameText.text = dinoName.ToUpper();
    }
    
}
