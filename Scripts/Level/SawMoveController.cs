using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SawMoveController : MonoBehaviour
{
    [SerializeField] private Transform sawTransform;
    [SerializeField] private float moveConstraint;
    [SerializeField] private float speed;
    [Range(-1, 1)] [SerializeField] private int direction;
    private Vector3 _moveVector;

    public event Action<int> SawDirectionChange;

    private void Awake()
    {
        if (direction == 0)
        {
            if (Random.value < 0.5f)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
        }
    }

    private void Start()
    {
        SawDirectionChange?.Invoke(direction);
    }

    private void Update()
    {
        _moveVector = sawTransform.localPosition + Vector3.right * (speed * direction * Time.deltaTime);
        sawTransform.localPosition = _moveVector;
        if (_moveVector.x > moveConstraint)
        {
            direction = -1;
            SawDirectionChange?.Invoke(direction);
        }
        else if (_moveVector.x < -moveConstraint)
        {
            direction = 1;
            SawDirectionChange?.Invoke(direction);
        }
    }
}