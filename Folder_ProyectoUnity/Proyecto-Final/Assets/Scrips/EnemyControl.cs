using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyControl : MonoBehaviour
{
    public EnemyStats stats = new EnemyStats();
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float timeSinceStart = 0f;

    public AnimationCurve speedCurve;

    public static event Action<int> OnEnemyDeath;

    void Start()
    {
        if (stats == null)
        {
            stats = new EnemyStats();
        }
    }

    void Update()
    {
        if (currentWaypointIndex >= waypoints.Length)
        {
            return;
        }

        Transform target = waypoints[currentWaypointIndex];

        timeSinceStart += Time.deltaTime;

        float adjustedSpeed = stats.speed * speedCurve.Evaluate(timeSinceStart);

        Vector3 direction = (target.position - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, adjustedSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            ++currentWaypointIndex;
        }
    }

    public void TakeDamage(GameObject source, int damage)
    {
        stats.health -= damage;
        if (stats.health <= 0)
        {
            Die();
            Destroy(gameObject);
        }
    }

    void Die()
    {
        OnEnemyDeath?.Invoke(stats.pointsOnDeath);
        GameManager.Instance.AddScore(stats.pointsOnDeath);
        GiveMoneyToPlayer(stats.pointsOnDeath);
    }

    void OnDestroy()
    {
        WaveController.Instance.OnEnemyDestroyed();
    }

    void GiveMoneyToPlayer(int amount)
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.AddMoney(amount);
        }
    }
}
