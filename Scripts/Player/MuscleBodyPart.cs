using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleBodyPart : DinoBodyPart
{
    private SkinnedMeshRenderer _renderer;
    private IEnumerator _currentCor;
    private void Awake()
    {
        _renderer = GetComponent<SkinnedMeshRenderer>();
    }

    public override void SetActive(bool isActive)
    {
        //gameObject.SetActive(isActive);
        if (_currentCor != null)
        {
            StopCoroutine(_currentCor);
        }
        if (isActive)
        {
            gameObject.SetActive(true);
            _currentCor = BlendShapeShowCor(0.5f);
        }
        else
        {
            _currentCor = BlendShapeHideCor(.5f);
        }

        StartCoroutine(_currentCor);
    }

    public override void SetActiveImmediate(bool isActive)
    {
        gameObject.SetActive(isActive);
        _renderer.SetBlendShapeWeight(0, isActive ? 0 : 100);

    }

    private IEnumerator BlendShapeHideCor(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            _renderer.SetBlendShapeWeight(0, Mathf.Lerp(0f, 100f, t / time));

            yield return null;
        }

        _renderer.SetBlendShapeWeight(0, 100f);
        gameObject.SetActive(false);
    }

    private IEnumerator BlendShapeShowCor(float time)
    {
        for (float t = time; t > 0; t -= Time.deltaTime)
        {
            _renderer.SetBlendShapeWeight(0, Mathf.Lerp(0f, 100f, t / time));

            yield return null;
        }

        _renderer.SetBlendShapeWeight(0, 0f);
    }
}