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

    public void TakeDamage(GameObject enemy, int damage)
    {
        if (enemy == gameObject)
        {
            stats.health -= damage;
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
