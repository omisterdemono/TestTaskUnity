using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    private Transform _player;

    public List<GameObject> Enemies = new List<GameObject>();
    public float minDistance = 10f;
    public float maxDistance = 20f;
    public int maxAttempts = 30;
    public float minSpawnDistanceFromEachOther = 5f;

    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
    }

    public void Spawn()
    {
        Vector3 spawnPosition = GetRandomPositionNearPlayer(_player.position, minDistance, maxDistance);
        if (spawnPosition != Vector3.zero)
        {
            Enemies.Add(Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity));
        }
        else
        {
            Debug.LogError("Failed to find a valid spawn position");
        }
    }

    Vector3 GetRandomPositionNearPlayer(Vector3 playerPosition, float minDist, float maxDist)
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * maxDist;
            randomDirection += playerPosition;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, maxDist, NavMesh.AllAreas))
            {
                float distanceFromPlayer = Vector3.Distance(playerPosition, hit.position);
                if (distanceFromPlayer >= minDist && distanceFromPlayer <= maxDist)
                {
                    if (IsValidSpawnPosition(hit.position))
                    {
                        return hit.position;
                    }
                }
            }
        }
        return Vector3.zero;
    }

    bool IsValidSpawnPosition(Vector3 position)
    {
        foreach (GameObject enemy in Enemies)
        {
            if (Vector3.Distance(position, enemy.transform.position) < minSpawnDistanceFromEachOther)
            {
                return false;
            }
        }
        return true;
    }
}
