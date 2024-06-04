using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public EnemyStats stats = new EnemyStats();
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void Update()
    {
        if (currentWaypointIndex >= waypoints.Length)
        {
            return;

        }

        Transform target = waypoints[currentWaypointIndex];

        transform.position = Vector3.MoveTowards(transform.position, target.position, stats.speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            ++currentWaypointIndex;

        }
    }

    public void TakeDamage(GameObject enemy, Projectile projectile)
    {
        if (enemy == gameObject)
        {
            stats.health -= projectile.config.damage;

            if (stats.health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnDestroy()
    {
        WaveController.Instance.OnEnemyDestroyed();
    }
}
