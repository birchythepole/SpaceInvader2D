using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Wave Config Menu")]
public class WaveConfig : ScriptableObject {
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField][Range (0,10)] float timeBetweenSpawns = 0.5f;
    [SerializeField][Range(0, 10)] float spawnRandomFactor = 0.3f;
    [SerializeField][Range(0, 10)] float moveSpeed = 2f;
    [SerializeField][Range(0, 10)] int numberOfEnemies = 5;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public GameObject GetPathPrefab() { return pathPrefab; }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public float GetMoveSpeed() { return moveSpeed; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
}
