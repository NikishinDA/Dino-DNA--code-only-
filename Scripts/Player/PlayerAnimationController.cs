using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<PlayerObstacleHitEvent>(OnObstacleHit);
        EventManager.AddListener<PlayerFireHitEvent>(OnFireHit);
        EventManager.AddListener<FinisherPlayerInPosition>(OnPlayerInPosition);
        _animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<PlayerObstacleHitEvent>(OnObstacleHit);
        EventManager.RemoveListener<PlayerFireHitEvent>(OnFireHit);
        EventManager.RemoveListener<FinisherPlayerInPosition>(OnPlayerInPosition);

    }

    private void OnPlayerInPosition(FinisherPlayerInPosition obj)
    {
        _animator.SetTrigger("Cheer");
    }

    private void OnFireHit(PlayerFireHitEvent obj)
    {
        _animator.SetTrigger("Damage");
    }

    private void OnObstacleHit(PlayerObstacleHitEvent obj)
    {
        _animator.SetTrigger("Damage");
    }

    private void OnGameStart(GameStartEvent obj)
    {
        _animator.SetTrigger("Start");
    }
}
