using UnityEngine;

public class WorldMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private float _startSpeed;

    private void Start()
    {
        _startSpeed = moveSpeed;
        StopMoving();
        GameEvents.Instance.OnGameStarted += SetNormalSpeed;
        GameEvents.Instance.OnGameLosed += StopMoving;
        GameEvents.Instance.OnGameStoped += StopMoving;
        GameEvents.Instance.OnInvincibility += InvincibilitySpeed;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGameStarted -= SetNormalSpeed;
        GameEvents.Instance.OnGameLosed -= StopMoving;
        GameEvents.Instance.OnGameStoped -= StopMoving;
        GameEvents.Instance.OnInvincibility -= InvincibilitySpeed;
    }

    private void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    private void SetDoublingSpeed()
    {
        moveSpeed *= 2f;
    }

    private void SetNormalSpeed()
    {
        moveSpeed = _startSpeed;
    }

    private void StopMoving()
    {
        moveSpeed = 0f;
    }

    private void InvincibilitySpeed(bool isInvincible)
    {
        if (isInvincible)
        {
            SetDoublingSpeed();
        }
        else
        {
            SetNormalSpeed();
        }
    }
}
