using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject finisherScreen;
    [SerializeField] private GameObject tutorScreen;

    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<FinisherPlayerInPosition>(OnFinisherPlayerInPosition);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<FinisherPlayerInPosition>(OnFinisherPlayerInPosition);
    }

    private void OnFinisherPlayerInPosition(FinisherPlayerInPosition obj)
    {
        finisherScreen.SetActive(true);
    }

    private void OnGameOver(GameOverEvent obj)
    {
        gameScreen.SetActive(false);
        finisherScreen.SetActive(false);
        if (obj.IsWin)
        {
            winScreen.SetActive(true);
        }
        else
        {
            loseScreen.SetActive(true);
        }
    }

    private void OnGameStart(GameStartEvent obj)
    {
        startScreen.SetActive(false);
        gameScreen.SetActive(true);
        
        int level = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.Level);
        if (level == 1)
        {
            StartCoroutine(TutorWaitCor(0.5f));
        }
    
    }

    void Start()
    {
        startScreen.SetActive(true);
    }

    private IEnumerator TutorWaitCor(float time)
    {
        yield return new WaitForSeconds(time);
        tutorScreen.SetActive(true);

    }
}