using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenCollectionButton : MonoBehaviour
{
    [SerializeField] private Button collectionButton;

    private void Awake()
    {
        collectionButton.onClick.AddListener(OnCollectionButtonClick);
    }

    private void OnCollectionButtonClick()
    {
        SceneManager.LoadScene(1);
    }
}
