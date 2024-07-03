using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class WaveController : MonoBehaviour
{
    public Transform[] spawnPoints;
    public int enemyCount;
    public int currentWave = 0;
    public TimerController timerController;
    public UnityEvent onWaveCompleted;
    public float spawnInterval = 1f;
    public Light sceneLight;
    public float colorChangeDuration = 1.0f;
    public GameObject[] enemyPrefabs;

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
        onWaveCompleted.AddListener(OnWaveCompleted);
    }

    public void StartWave()
    {
        AnimateLightChange();
        Wave newWave = GenerateWave(currentWave + 1);
        StartCoroutine(SpawnWave(newWave));
        ++currentWave;
    }

    private Wave GenerateWave(int waveNumber)
    {
        SimplyLinkedList<EnemyType> enemies = new SimplyLinkedList<EnemyType>(); //O(1)
        int enemyCount = waveNumber * 5; //O(1)

        for (int i = 0; i < enemyCount; ++i) //O(n)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length); //O(1)
            enemies.InsertNodeAtEnd(new EnemyType(enemyPrefabs[randomIndex], 1)); //O(n)
        }

        return new Wave(enemies); //O(1)
    }//O(n^2)

    private void AnimateLightChange()
    {
        sceneLight.DOIntensity(1.5f, colorChangeDuration).SetLoops(2, LoopType.Yoyo);
        sceneLight.DOColor(Color.blue, colorChangeDuration).SetLoops(2, LoopType.Yoyo);
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemies.Count; ++i)
        {
            EnemyType enemyType = wave.enemies.Get(i);
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

    private void OnWaveCompleted()
    {
        GameManager.Instance.IncrementLevel();
    }
}
