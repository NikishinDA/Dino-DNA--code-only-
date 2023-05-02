using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawEffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftEffect;
    [SerializeField] private ParticleSystem rightEffect;
    [SerializeField] private Animator sawAnimator;

    public void SpinRight()
    {
        leftEffect.Stop();
        rightEffect.Play();
        sawAnimator.SetTrigger("Right");
    }

    public void SpinLeft()
    {
        leftEffect.Play();
        rightEffect.Stop();
        sawAnimator.SetTrigger("Left");
    }
}
