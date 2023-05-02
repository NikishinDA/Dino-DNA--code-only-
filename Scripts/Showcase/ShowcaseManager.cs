using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowcaseManager : MonoBehaviour
{
    [SerializeField] private Transform dinoHolder;

    [SerializeField] private GameObject[] dinos;
    [SerializeField] private GameObject placeholder;

    private int _skinNum;
    private int _fixedSkinNum;
    private readonly List<GameObject> _spawnedDinos = new List<GameObject>();
    private readonly List<string> _unlockedDinoNames = new List<string>();
    private int _current;
    [SerializeField] private string[] dinoNames;


    private void Awake()
    {
        _skinNum = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.SkinNumber);
        _fixedSkinNum = Mathf.Clamp(_skinNum, 0, dinos.Length);
        int i;
        for (i = 0; i < _fixedSkinNum; i++)
        {
            _spawnedDinos.Add(Instantiate(dinos[i], dinoHolder));
            _unlockedDinoNames.Add(dinoNames[i]);
        }

        for (int j = i; j < dinos.Length; j++)
        {
            _spawnedDinos.Add(Instantiate(placeholder, dinoHolder));
            _unlockedDinoNames.Add("???");

        }

        _current = 0;
        _spawnedDinos[_current].SetActive(true);
        EventManager.AddListener<ShowcaseSwitchEvent>(OnShowcaseSwitch);
        EventManager.AddListener<ShowcaseSelectEvent>(OnShowcaseSelect);
        /*if (_skinNum < _spawnedDinos.Count)
        {
            selectButton.interactable = false;
        }
        else
        {
            selectButton.onClick.AddListener(OnSelectButtonClick);
        }*/
    }

    private void Start()
    {
        var evt = GameEventsHandler.ShowcaseSkinInfoEvent;
        evt.AllowSelect = !(_skinNum < _spawnedDinos.Count);
        EventManager.Broadcast(evt);
        var nameevt = GameEventsHandler.ShowcaseSkinShowEvent;
        nameevt.Name = _unlockedDinoNames[_current];
        EventManager.Broadcast(nameevt);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<ShowcaseSwitchEvent>(OnShowcaseSwitch);
        EventManager.RemoveListener<ShowcaseSelectEvent>(OnShowcaseSelect);
    }

    private void OnShowcaseSelect(ShowcaseSelectEvent obj)
    {
        PlayerPrefs.SetInt(PlayerPrefsStrings.SkinNumber.Name, _current + _spawnedDinos.Count);
    }

    private void OnShowcaseSwitch(ShowcaseSwitchEvent obj)
    {
        if (obj.IsNext)
        {
            ShowNext();
        }
        else
        {
            ShowPrevious();
        }

        var evt = GameEventsHandler.ShowcaseSkinShowEvent;
        evt.Name = _unlockedDinoNames[_current];
        EventManager.Broadcast(evt);
    }

    private void ShowNext()
    {
        _spawnedDinos[_current].SetActive(false);
        if (_current + 1 >= _spawnedDinos.Count)
        {
            _current = 0;
        }
        else
        {
            _current++;
        }

        _spawnedDinos[_current].SetActive(true);
    }

    private void ShowPrevious()
    {
        _spawnedDinos[_current].SetActive(false);
        if (_current - 1 < 0)
        {
            _current = _spawnedDinos.Count - 1;
        }
        else
        {
            _current--;
        }

        _spawnedDinos[_current].SetActive(true);
    }
}