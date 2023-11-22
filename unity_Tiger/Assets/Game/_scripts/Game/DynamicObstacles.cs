using UnityEngine;

public class DynamicObstacles : MonoBehaviour
{
    private float _moveSpeed = 2f;
    private float _startSpeed;

    private void Start()
    {
        _startSpeed = _moveSpeed;
        GameEvents.Instance.OnGameStarted += SetNormalSpeed;
        GameEvents.Instance.OnGameLosed += StopMoving;
        GameEvents.Instance.OnGameStoped += StopMoving;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGameStarted -= SetNormalSpeed;
        GameEvents.Instance.OnGameLosed -= StopMoving;
        GameEvents.Instance.OnGameStoped -= StopMoving;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * _moveSpeed * Time.deltaTime);
    }

    private void SetNormalSpeed()
    {
        _moveSpeed = _startSpeed;
    }

    private void StopMoving()
    {
        _moveSpeed = 0f;
    }
}
