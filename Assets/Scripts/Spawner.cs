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
        //Get reference on player
        _player = FindObjectOfType<Player>().transform;
    }

    //Function that spawns enemies around the player
    public void Spawn()
    {
        Vector3 spawnPosition = GetRandomPositionNearPlayer(_player.position, minDistance, maxDistance);
        if (spawnPosition != Vector3.zero)
        {
            //Saving enemies references to avoid them spawn on each other
            Enemies.Add(Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity));
        }
        else
        {
            Debug.LogError("Failed to find a valid spawn position");
        }
    }

    //Function that calculate position to spawn enemy for maxAttempts
    Vector3 GetRandomPositionNearPlayer(Vector3 playerPosition, float minDist, float maxDist)
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * maxDist;
            randomPosition += playerPosition;

            NavMeshHit hit;
            //Find nearest position to spawn on NavMesh
            if (NavMesh.SamplePosition(randomPosition, out hit, maxDist, NavMesh.AllAreas))
            {
                //Get distance between player and random position
                float distanceFromPlayer = Vector3.Distance(playerPosition, hit.position);
                //Check if distance is between minDist and maxDist
                if (distanceFromPlayer >= minDist && distanceFromPlayer <= maxDist)
                {
                    //Check for enemy collision
                    if (IsValidSpawnPosition(hit.position))
                    {
                        return hit.position;
                    }
                }
            }
        }
        return Vector3.zero;
    }

    //Function that check if position is allowed
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
