using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenMoneyController : MonoBehaviour
{
    [SerializeField] private Text moneyText;

    private void OnEnable()
    {
        moneyText.text = VarSaver.MoneyCollected.ToString();
    }
}
