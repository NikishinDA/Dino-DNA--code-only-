using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrapEffectController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayEffect()
    {
        animator.SetTrigger("Activate");
    }
}
