using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenUpgradeManager : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Text upgradeCostText;
    private int _totalMoney;
    private int _upgradeLevel;
    private int _upgradeCost;
    private void Awake()
    {
        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
        _totalMoney = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.MoneyTotal);
        _upgradeLevel = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.UpgradeLevel);
        _upgradeCost = 6 + Mathf.RoundToInt(Mathf.Pow(1.25f, _upgradeLevel)) ;//Mathf.Pow(2 , _upgradeLevel);
        upgradeCostText.text = _upgradeCost.ToString();
    }

    private void OnUpgradeButtonClick()
    {
        if (_totalMoney >= _upgradeCost)
        {
            _totalMoney -= _upgradeCost;
            _upgradeLevel++;
            _upgradeCost = 6 + Mathf.RoundToInt(Mathf.Pow(1.25f, _upgradeLevel));
            upgradeCostText.text = _upgradeCost.ToString();

            PlayerPrefs.SetInt(PlayerPrefsStrings.UpgradeLevel.Name, _upgradeLevel);
            PlayerPrefs.SetInt(PlayerPrefsStrings.MoneyTotal.Name, _totalMoney);
            PlayerPrefs.Save();
            var evt = GameEventsHandler.UpgradeButtonPressEvent;
            evt.UpgradeLevel = _upgradeLevel;
            evt.MoneyTotal = _totalMoney;
            EventManager.Broadcast(evt);
        }
    }
}
