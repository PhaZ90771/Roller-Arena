using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
    public GameObject[] PowerupPrefabs;

    private float spawnRange = 9;
    private int waveNumber = 0;

    private void Update()
    {
        if (!Enemy.EnemyList.Any())
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
            var index = Random.Range(0, EnemyPrefabs.Length);
            Instantiate(EnemyPrefabs[index], GenerateSpawnPosition(), EnemyPrefabs[index].transform.rotation);
        }
    }

    private void SpawnPowerup()
    {
        var index = Random.Range(0, PowerupPrefabs.Length);
        Instantiate(PowerupPrefabs[index], GenerateSpawnPosition(), PowerupPrefabs[index].transform.rotation);
    }

    private void SpawnWave()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerup();
    }
}
