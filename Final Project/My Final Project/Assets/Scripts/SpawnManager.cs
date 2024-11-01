using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    private float spawnRange = 100;
    public int enemyCount;
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 randomPos = new Vector3(0, 4, 0);
        do
        {
            float spawnPosX = Random.Range(-spawnRange, spawnRange);
            float spawnPosY = Random.Range(-spawnRange, spawnRange);
            randomPos = new Vector3(spawnPosX, 4, spawnPosX);
        }
        while (Vector3.Distance(randomPos, player.transform.position) < 30);
        return randomPos;
    }
    
    private float GenerateSpawnRotation()
    {
        float rotation = Random.Range(-180,180);
        return rotation;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            enemy.transform.Rotate(0, 0, GenerateSpawnRotation(), Space.Self);

        }
    }
}
