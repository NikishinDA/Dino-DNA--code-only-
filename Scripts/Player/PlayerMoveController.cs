using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMoveController : MonoBehaviour
{
    [Header("X Movement")] [SerializeField]
    private float maxSpeedX;

    [SerializeField] private float movementBoundary;

    [Header("Y Movement")] [SerializeField]
    private float gravityForce;

    [Header("Z Movement")] [SerializeField]
    private float startSpeedZ = 10;

    [SerializeField] private float slowdownSpeed;
    [SerializeField] private float acceleration;
    private float _speedZ;

    private float _newX;
    private float _oldX;
    private Vector3 _newPosition;
    private PlayerInputManager _inputManager;
    private CharacterController _cc;
    private bool _move;

    private float _touchDelta;

    //[SerializeField] private float gravityForce;
    //[SerializeField] private CinemachineVirtualCamera vCamera;
    private void Awake()
    {
        _inputManager = GetComponent<PlayerInputManager>();
        _cc = GetComponent<CharacterController>();
        _newPosition = new Vector3();
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<DebugCallEvent>(OnDebugCall);
        EventManager.AddListener<PlayerFinishReachedEvent>(OnFinisherStart);
        _speedZ = startSpeedZ;
    }

    private void OnDebugCall(DebugCallEvent obj)
    {
        _speedZ = obj.FloatValues[0];
        maxSpeedX = obj.FloatValues[1];
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<DebugCallEvent>(OnDebugCall);
        EventManager.RemoveListener<PlayerFinishReachedEvent>(OnFinisherStart);
    }

    private void OnFinisherStart(PlayerFinishReachedEvent obj)
    {
        _move = false;
    }


    private void OnGameOver(GameOverEvent obj)
    {
        _move = false;
    }

    private void OnGameStart(GameStartEvent obj)
    {
        _move = true;
    }

    private void Update()
    {
        if (!_move) return;
            _touchDelta = _inputManager.GetTouchDelta();
            _newX = maxSpeedX * _touchDelta;
            if (Mathf.Abs(transform.position.x + _newX) >= movementBoundary)
            {
                _newX = 0;
            }

            _newPosition.x = _newX;
        /*}
        else
        {
            _newPosition.x = -transform.position.x * maxSpeedX * Time.deltaTime;
        }*/

        _newPosition.z = _speedZ * Time.deltaTime;
        if (!CheckIfGrounded())
        {
            _newPosition.y = -gravityForce * Time.deltaTime;
        }

        _cc.Move(_newPosition);
        if (_speedZ < startSpeedZ)
        {
            _speedZ = Mathf.Clamp(_speedZ + acceleration * Time.deltaTime, 0, startSpeedZ);
        }
    }

    private void SlowDownEffect()
    {
        _speedZ = slowdownSpeed;
    }

    private bool CheckIfGrounded()
    {
        RaycastHit hit;
        bool res = Physics.SphereCast(transform.position, 1f, Vector3.down, out hit,
            1f,
            LayerMask.NameToLayer("Ground"));
        return res;
    }
}