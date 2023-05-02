using System;
using System.Collections;
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    [Header("Scores")] [SerializeField] private float scorePerDNA;
    [SerializeField] private float damage;

    //[Space] [SerializeField] private BodyPartsManager bodyPartsManager;

    [Space] [SerializeField] private float gateEffectDuration;
    [Space] [SerializeField] private float gateEffectValue;
    [SerializeField] private float[] scoresThresholds;
    [SerializeField] private float addPerUpgrade;
    private float _score;
    private int _currentStage;

    private int _upgradeLevel;
    private float _startModifier = 1f;

    private void Awake()
    {
        EventManager.AddListener<PlayerDNACollectEvent>(OnDNACollect);
        EventManager.AddListener<PlayerObstacleHitEvent>(OnObstacleHit);
        EventManager.AddListener<PlayerFireHitEvent>(OnFireHit);
        EventManager.AddListener<GateEvent>(OnGateEvent);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<DebugCallEvent>(OnDebugCall);
        EventManager.AddListener<UpgradeButtonPressEvent>(OnUpgradeButtonPress);
        EventManager.AddListener<FinisherPlayerInPosition>(OnFinisherPlayerinPos);

        _upgradeLevel = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.UpgradeLevel);
        _score = PlayerPrefsStrings.GetFloatValue(PlayerPrefsStrings.PlayerScore);
        if (_score < 1f)
        {
            _score = _upgradeLevel * addPerUpgrade;
        }

        _currentStage = FindStage(_score);

        int skinNum = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.SkinNumber);
        switch (skinNum)
        {
            case 0:
            {
                _startModifier = 4f;
            }
                break;
            case 1:
            {
                _startModifier = 1.5f;
            }
                break;
        }
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerDNACollectEvent>(OnDNACollect);
        EventManager.RemoveListener<PlayerObstacleHitEvent>(OnObstacleHit);
        EventManager.RemoveListener<PlayerFireHitEvent>(OnFireHit);
        EventManager.RemoveListener<GateEvent>(OnGateEvent);
        EventManager.RemoveListener<FinisherPlayerInPosition>(OnFinisherPlayerinPos);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<DebugCallEvent>(OnDebugCall);
        EventManager.RemoveListener<UpgradeButtonPressEvent>(OnUpgradeButtonPress);
    }

    private void OnFinisherPlayerinPos(FinisherPlayerInPosition obj)
    {
        if (_score >= scoresThresholds[scoresThresholds.Length - 1])
        {
            EventManager.Broadcast(GameEventsHandler.FinisherPlayerProgressInfoEvent);
            _score = 0;
            PlayerPrefs.SetInt(PlayerPrefsStrings.SkinNumber.Name,
                PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.SkinNumber) + 1);
        }

        PlayerPrefs.SetFloat(PlayerPrefsStrings.PlayerScore.Name, _score);
        PlayerPrefs.Save();
    }

    private void OnDebugCall(DebugCallEvent obj)
    {
        scorePerDNA = obj.IntValues[2];
        damage = obj.IntValues[3];
        gateEffectValue = obj.FloatValues[4];
        gateEffectDuration = obj.FloatValues[5];
    }

    private void Start()
    {
        var evt = GameEventsHandler.PlayerScoreChangeEvent;
        evt.Value = _score;
        evt.IsImmediate = true;
        evt.Stage = _currentStage;
        EventManager.Broadcast(evt);
    }

    private void OnUpgradeButtonPress(UpgradeButtonPressEvent obj)
    {
        if (obj.UpgradeLevel * addPerUpgrade > _score)
        {
            _score = _upgradeLevel * addPerUpgrade;

            var evt = GameEventsHandler.PlayerScoreChangeEvent;
            evt.Value = _score;
            evt.IsImmediate = false;
            evt.Stage = GetStage(_score);
            EventManager.Broadcast(evt);
        }
    }

    private void OnGameStart(GameStartEvent obj)
    {
        var evt = GameEventsHandler.PlayerScoreChangeEvent;
        evt.Value = _score;
        evt.IsImmediate = false;
        evt.Stage = _currentStage;
        EventManager.Broadcast(evt);
    }

    private void OnGameOver(GameOverEvent obj)
    {
        if (obj.IsWin) return;
        if (_score >= scoresThresholds[scoresThresholds.Length - 1])
        {
            _score = 0;
            PlayerPrefs.SetInt(PlayerPrefsStrings.SkinNumber.Name,
                PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.SkinNumber) + 1);
        }

        PlayerPrefs.SetFloat(PlayerPrefsStrings.PlayerScore.Name, _score);
        PlayerPrefs.Save();
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            ChangeValue(+1f);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            ChangeValue(-1f);
        }
    }
#endif
    private void OnGateEvent(GateEvent obj)
    {
        StartCoroutine(GateEffectCor(gateEffectDuration, obj.ChangeValue * gateEffectValue));
    }

    private void OnObstacleHit(PlayerObstacleHitEvent obj)
    {
        ChangeValue(-damage);
    }

    private void OnFireHit(PlayerFireHitEvent obj)
    {
        ChangeValue(-damage);
    }

    private void OnDNACollect(PlayerDNACollectEvent obj)
    {
        //if (_score < 100f)
            ChangeValue(scorePerDNA * _startModifier);
       // else
        //{
        //    var evt = GameEventsHandler.MoneyCollectEvent;
         //   evt.PlayerPosition = obj.PlayerPosition;
         //   EventManager.Broadcast(evt);
        //}
    }

    private IEnumerator GateEffectCor(float time, float value)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            ChangeValue(value * _startModifier * Time.deltaTime / time);
            yield return null;
        }
    }

    private void ChangeValue(float value)
    {
        _score = Mathf.Clamp(_score + value, 0f, 100f);
        var evt = GameEventsHandler.PlayerScoreChangeEvent;
        evt.Value = _score;
        evt.Stage = GetStage(_score);
        evt.IsImmediate = false;
        EventManager.Broadcast(evt);
        //bodyPartsManager.ToggleBodyParts(_score);
        if (value < 0 && _score < scoresThresholds[0])
        {
            _score = 0;
            var goevt = GameEventsHandler.GameOverEvent;
            goevt.IsWin = false;
            EventManager.Broadcast(goevt);
        }
    }

    private int GetStage(float score)
    {
        if (_currentStage < scoresThresholds.Length && score > scoresThresholds[_currentStage])
        {
            _currentStage++;
        }
        else if (_currentStage > 0 && score < scoresThresholds[_currentStage - 1])
        {
            _currentStage--;
        }

        return _currentStage;
    }

    private int FindStage(float score)
    {
        int stage = 0;
        foreach (var threshold in scoresThresholds)
        {
            if (score >= threshold)
            {
                stage++;
            }
            else
            {
                break;
            }
        }

        return stage;
    }
}