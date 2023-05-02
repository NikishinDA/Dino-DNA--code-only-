using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] modelPrefabs;

    private void Awake()
    {
        int skinNum = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.SkinNumber) % modelPrefabs.Length;
        Instantiate(modelPrefabs[skinNum], transform);
    }
}
