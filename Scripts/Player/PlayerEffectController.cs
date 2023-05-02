using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerEffectController : MonoBehaviour
{
    private CinemachineImpulseSource _impulseSource;
    [SerializeField] private ParticleSystem moneyEffect;
    [SerializeField] private ParticleSystem dnaEffect;
    [SerializeField] private ParticleSystem fireEffect;
    [SerializeField] private ParticleSystem decayEffect;
    [SerializeField] private ParticleSystem growEffect;

    private void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        EventManager.AddListener<PlayerObstacleHitEvent>(OnObstacleHit);
        EventManager.AddListener<PlayerFireHitEvent>(OnFireHit);
        EventManager.AddListener<PlayerDNACollectEvent>(OnDnaCollect);
        EventManager.AddListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.AddListener<GateEvent>(OnGateEvent);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerObstacleHitEvent>(OnObstacleHit);
        EventManager.RemoveListener<PlayerFireHitEvent>(OnFireHit);
        EventManager.RemoveListener<PlayerDNACollectEvent>(OnDnaCollect);
        EventManager.RemoveListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.RemoveListener<GateEvent>(OnGateEvent);

    }

    private void OnGateEvent(GateEvent obj)
    {
        if (obj.ChangeValue < 0)
        {
            Taptic.Failure();
            decayEffect.Play();
        }
        else
        {
            Taptic.Success();
            growEffect.Play();
        }
    }

    private void OnMoneyCollect(MoneyCollectEvent obj)
    {
        Taptic.Light();
        moneyEffect.Play();
    }

    private void OnDnaCollect(PlayerDNACollectEvent obj)
    {
        Taptic.Light();
        dnaEffect.Play();
    }

    private void OnFireHit(PlayerFireHitEvent obj)
    {
        _impulseSource.GenerateImpulse();
        Taptic.Failure();
        fireEffect.Play();
    }

    private void OnObstacleHit(PlayerObstacleHitEvent obj)
    {
        _impulseSource.GenerateImpulse();
        Taptic.Failure();
    }
}
