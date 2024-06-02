using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public float timeBetweenWaves = 5f; 
    public WaveController waveController; 

    void Start()
    {
        StartCoroutine(StartWaves());
    }

    IEnumerator StartWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            waveController.SpawnWave();
        }
    }
}
