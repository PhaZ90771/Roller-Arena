using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;

    private float spawnRange = 9;
    private int enemyCount;
    private int waveNumber = 0;

    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnWave();
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnX, 0, spawnZ);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            var index = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[index], GenerateSpawnPosition(), enemyPrefabs[index].transform.rotation);
        }
    }

    private void SpawnPowerup()
    {
        var index = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[index], GenerateSpawnPosition(), powerupPrefabs[index].transform.rotation);
    }

    private void SpawnWave()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerup();
    }
}
