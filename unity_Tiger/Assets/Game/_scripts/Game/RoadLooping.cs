using UnityEngine;

public class RoadLooping : MonoBehaviour
{
    [SerializeField] private float roadSpeed;
    private Renderer _roadRenderer;
    private float _startSpeed;

    private void Start()
    {
        _startSpeed = roadSpeed;
        StopMoving();
        _roadRenderer = GetComponent<Renderer>();
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
        LoopRoad();
    }

    private void LoopRoad()
    {
        _roadRenderer.material.mainTextureOffset += new Vector2(roadSpeed * Time.deltaTime, 0f);
    }

    private void InvincibilitySpeed(bool isInvincible)
    {
        if(isInvincible)
        {
            SetDoublingSpeed();
        }
        else
        {
            SetNormalSpeed();
        }
    }

    private void SetDoublingSpeed()
    {
        roadSpeed *= 2f;
    }

    private void SetNormalSpeed()
    {
        roadSpeed = _startSpeed;
    }

    private void StopMoving()
    {
        roadSpeed = 0f;
    }
}
