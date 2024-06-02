using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public int enemiesPerWave = 10; 

    public int currentWaveNumber { get; private set; } = 0; 

    public void SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
        currentWaveNumber++;
    }
}
