using System;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private WorldMover worldMover;
    private float _previousYPosition;
    public float totalScore = 0.0f;
    private float _playerDistance;
    private float _obstacleCost = 10f;
    private float _scoreMultiplier = 1f;

    private void Start()
    {
        GameEvents.Instance.OnGameLosed += SaveGameScore;
        GameEvents.Instance.OnSaveZonePassed += AddObstacleCost;
        GameEvents.Instance.OnDoubleScore += DobuleScore;

        InvokeRepeating("SetTotalScore", 0.1f, 0.1f);
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnSaveZonePassed -= AddObstacleCost;
        GameEvents.Instance.OnDoubleScore -= DobuleScore;
        GameEvents.Instance.OnGameLosed -= SaveGameScore;
    }

    private void Update()
    {
        CountPlayerDistance();
    }

    private void CountPlayerDistance()
    {
        var currentYPosition = worldMover.transform.position.y;
        if (_previousYPosition != 0)
        {
            _playerDistance += MathF.Abs(currentYPosition - _previousYPosition) * _scoreMultiplier;
            _playerDistance = (float)Math.Round(_playerDistance, 2);
        }
        _previousYPosition = currentYPosition;
    }

    private void AddObstacleCost()
    {
        _playerDistance += (_obstacleCost * _scoreMultiplier);
    }

    private void SetTotalScore()
    {
        totalScore = _playerDistance;
    }

    private void DobuleScore()
    {
        _scoreMultiplier = 2f;
    }

    private void SaveGameScore()
    {
        SLS.Data.TotalCoinsAmount.Value += (int)totalScore;
    }

}
