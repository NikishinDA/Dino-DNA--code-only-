using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenDnaMeterController : MonoBehaviour
{
    [SerializeField] private Image fill;

    private void OnEnable()
    {
        StartCoroutine(FillCor(1f));
    }

    private IEnumerator FillCor(float time)
    {
        yield return new WaitForSeconds(0.5f);
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            fill.fillAmount = Mathf.Lerp(0, VarSaver.PlayerProgress, t / time);
            yield return null;
        }
    }
}
