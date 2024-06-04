using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public float waveInterval = 30f;
    private float timer;

    public WaveController waveController;

    private void OnEnable()
    {
        waveController.onWaveCompleted.AddListener(StartTimer);
    }

    private void OnDisable()
    {
        waveController.onWaveCompleted.RemoveListener(StartTimer);
    }

    void Start()
    {
        StartTimer();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                waveController.StartWave();
                timer = 0f;
            }
        }
    }

    public void StartTimer()
    {
        timer = waveInterval;
    }

    public void ResetTimer()
    {
        timer = 0f;
    }
}
