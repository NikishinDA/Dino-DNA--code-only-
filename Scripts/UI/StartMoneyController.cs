using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMoneyController : MonoBehaviour
{
    [SerializeField] private Text moneyText;
    private int _totalMoney;
    private void Awake()
    {
        _totalMoney = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.MoneyTotal);
        moneyText.text = _totalMoney.ToString();
        EventManager.AddListener<UpgradeButtonPressEvent>(OnUpgradeButtonPress);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<UpgradeButtonPressEvent>(OnUpgradeButtonPress);

    }

    private void OnUpgradeButtonPress(UpgradeButtonPressEvent obj)
    {
        _totalMoney = obj.MoneyTotal;
        moneyText.text = _totalMoney.ToString();
    }
}
