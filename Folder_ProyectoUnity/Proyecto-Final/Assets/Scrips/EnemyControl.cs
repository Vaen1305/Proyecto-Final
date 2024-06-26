using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public EnemyStats stats = new EnemyStats();
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float timeSinceStart = 0f;

    public AnimationCurve speedCurve;

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

        transform.position = Vector3.MoveTowards(transform.position, target.position, adjustedSpeed * Time.deltaTime);

        if (transform.position == target.position)
        {
            ++currentWaypointIndex;
        }
    }

    public void TakeDamage(GameObject source, int damage)
    {
        stats.health -= damage;
        if (stats.health <= 0)
        {
            Destroy(gameObject);
        }
        if (stats.health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GameManager.Instance.AddScore(stats.pointsOnDeath);
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        WaveController.Instance.OnEnemyDestroyed();
    }
}
