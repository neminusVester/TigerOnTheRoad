using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> staticObstacles;
    [SerializeField] private GameObject saveZone;
    [SerializeField] private Transform obstacleHolder;
    [SerializeField] private List<GameObject> dynamicObstacles;
    private float _leftMinOffset = -1f;
    private float _leftMaxOffset = -0.9f;
    private float _rightMaxOffset = 1f;
    private float _rightMinOffset = 0.9f;
    private float _spawnDelay = 5f;

    private void Start()
    {
        GameEvents.Instance.OnGameStarted += StartRoutine;
        GameEvents.Instance.OnGameStoped += StopAllCoroutines;
        GameEvents.Instance.OnGameLosed += StopAllCoroutines;
        GameEvents.Instance.OnLostGameContinues += ClearAllObstacles;
        GameEvents.Instance.OnObstacleDestroyer += ObstacleDestroyer;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnLostGameContinues -= ClearAllObstacles;
        GameEvents.Instance.OnGameStoped -= StopAllCoroutines;
        GameEvents.Instance.OnGameLosed -= StopAllCoroutines;
        GameEvents.Instance.OnGameStarted -= StartRoutine;
        GameEvents.Instance.OnObstacleDestroyer -= ObstacleDestroyer;
    }

    private void StartRoutine()
    {
        StartCoroutine(RandomSpawnStaticObstacle());
        StartCoroutine(RandomSpawnDynamicObstacle());
    }

    private IEnumerator RandomSpawnStaticObstacle()
    {
        var obstaclesAmount = Random.Range(1, 3);
        if (obstaclesAmount < 2)
        {
            var sideToSpawn = Random.Range(1, 3);//if 1 - left side, if 2 - right side
            switch (sideToSpawn)
            {
                case 1:
                    InstantiateSingleObstacle(staticObstacles, _leftMinOffset, _leftMaxOffset);
                    break;
                case 2:
                    InstantiateSingleObstacle(staticObstacles, _rightMinOffset, _rightMaxOffset);
                    break;
            }
        }
        else
        {
            InstantiateSingleObstacle(staticObstacles, _leftMinOffset, _leftMaxOffset);
            InstantiateSingleObstacle(staticObstacles, _rightMinOffset, _rightMaxOffset);
        }

        InstantiateSaveZone();

        yield return new WaitForSeconds(_spawnDelay);

        StartCoroutine(RandomSpawnStaticObstacle());
    }

    private IEnumerator RandomSpawnDynamicObstacle()
    {
        yield return new WaitForSeconds(3f);
        GameEvents.Instance.StartPreparingDynamicObstacle();
        yield return new WaitForSeconds(4f);
        InstantiateSingleObstacle(dynamicObstacles, _leftMinOffset, _leftMaxOffset);
        InstantiateSingleObstacle(dynamicObstacles, _rightMinOffset, _rightMaxOffset);
        yield return new WaitForSeconds(7f);

        StartCoroutine(RandomSpawnDynamicObstacle());
    }

    private void InstantiateSingleObstacle(List<GameObject> obstacles, float minOffset, float maxOffset)
    {
        var obstacleIndex = Random.Range(0, obstacles.Count);
        var xPositionToSpawn = Random.Range(minOffset, maxOffset);
        var spawnPosition = new Vector3(xPositionToSpawn, transform.position.y, transform.position.z);
        var obstacle = Instantiate(obstacles[obstacleIndex], spawnPosition, Quaternion.identity);
        obstacle.transform.SetParent(obstacleHolder);
    }

    private void InstantiateSaveZone()
    {
        var zone = Instantiate(saveZone.gameObject, transform.position, Quaternion.identity);
        zone.transform.SetParent(obstacleHolder);
    }


    private void ObstacleDestroyer()
    {
        foreach (Transform child in obstacleHolder)
        {
            if (child.tag == "SaveZone")
            {
                GameEvents.Instance.PlayerPasedObstacle();
            }

            Destroy(child.gameObject);
        }
    }

    private void ClearAllObstacles()
    {
        foreach (Transform child in obstacleHolder)
        {
            Destroy(child.gameObject);
        }
    }
}
