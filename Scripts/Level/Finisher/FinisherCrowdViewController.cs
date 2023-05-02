using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FinisherCrowdViewController : MonoBehaviour
{
    [SerializeField] private GameObject femaleSkin;
    [SerializeField] private GameObject maleSkin;
    [SerializeField] private GameObject glasses;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        if (Random.value < 0.5f)
        {
            femaleSkin.SetActive(true);
        }
        else
        {
            maleSkin.SetActive(true);
        }
        if (Random.value < 0.5f)
        {
            glasses.SetActive(true);
        }
        
        _animator.SetInteger("Rand", Random.Range(0, 6));
    }
}
