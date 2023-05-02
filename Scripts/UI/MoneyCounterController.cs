using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounterController : MonoBehaviour
{
    [SerializeField] private Text moneyText;
    private int _currentMoney;
    [SerializeField] private Animator textAnimator;
    private void Awake()
    {
        EventManager.AddListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.AddListener<PlayerFinishReachedEvent>(OnFinishReached);
        EventManager.AddListener<CoinUiCollectEvent>(OnCoinUiCollect);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.RemoveListener<PlayerFinishReachedEvent>(OnFinishReached);
        EventManager.RemoveListener<CoinUiCollectEvent>(OnCoinUiCollect);

    }

    private void OnCoinUiCollect(CoinUiCollectEvent obj)
    {        
        //textAnimator.ResetTrigger("Bob");
        moneyText.text = _currentMoney.ToString();
        textAnimator.SetTrigger("Bob");
    }

    private void OnFinishReached(PlayerFinishReachedEvent obj)
    {
        PlayerPrefs.SetInt(PlayerPrefsStrings.MoneyTotal.Name,
            PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.MoneyTotal) + _currentMoney);
    }

    private void OnMoneyCollect(MoneyCollectEvent obj)
    {
        _currentMoney++;
        //moneyText.text = _currentMoney.ToString();
        VarSaver.MoneyCollected = _currentMoney;
    }
}
