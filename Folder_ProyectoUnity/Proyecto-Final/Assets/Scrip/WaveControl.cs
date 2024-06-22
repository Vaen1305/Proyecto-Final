using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class WaveController : MonoBehaviour
{
    public List<Wave> waves;
    public Transform[] spawnPoints;
    public int enemyCount;
    public int currentWave = 0;
    public TimerController timerController;
    public UnityEvent onWaveCompleted;
    public float spawnInterval = 1f;
    public Light sceneLight;
    public float colorChangeDuration = 1.0f;
    public static WaveController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (onWaveCompleted == null)
        {
            onWaveCompleted = new UnityEvent();
        }

    }

    public void StartWave()
    {
        if (currentWave >= waves.Count)
        {
            return;
        }

        AnimateLightChange();

        StartCoroutine(SpawnWave(waves[currentWave]));
        ++currentWave;
    }
    private void AnimateLightChange()
    {
        sceneLight.DOIntensity(1.5f, colorChangeDuration).SetLoops(2, LoopType.Yoyo);

        sceneLight.DOColor(Color.red, colorChangeDuration).SetLoops(2, LoopType.Yoyo);
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemies.Count; ++i)
        {
            EnemyType enemyType = wave.enemies[i];
            for (int j = 0; j < enemyType.count; ++j)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject enemyPrefab = enemyType.enemyPrefab;
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                ++enemyCount;
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    public void EndWave()
    {
        if (enemyCount <= 0)
        {
            onWaveCompleted.Invoke();

            if (currentWave < waves.Count)
            {

            }
            else
            {

            }
        }
    }

    public void OnEnemyDestroyed()
    {
        --enemyCount;
        if (enemyCount <= 0)
        {
            EndWave();
        }
    }
}
