using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorController : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;

        }
    }
}
