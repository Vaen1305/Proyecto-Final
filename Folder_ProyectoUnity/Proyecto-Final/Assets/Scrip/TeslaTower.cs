using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaTower : Tower
{
    public TeslaTowerConfig teslaConfig;
    private Queue<EnemyControl> enemiesInRange = new Queue<EnemyControl>();
    private SphereCollider attackRange;

    void Start()
    {
        attackRange = gameObject.AddComponent<SphereCollider>();
        attackRange.isTrigger = true;
        attackRange.radius = teslaConfig.attackRadius;

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        StartCoroutine(AttackEnemies());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyControl enemy = other.GetComponent<EnemyControl>();
            if (enemy != null && !enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Enqueue(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyControl enemy = other.GetComponent<EnemyControl>();
            if (enemy != null && enemiesInRange.Contains(enemy))
            {
                Queue<EnemyControl> newQueue = new Queue<EnemyControl>();
                while (enemiesInRange.Count > 0)
                {
                    EnemyControl dequeuedEnemy = enemiesInRange.Dequeue();
                    if (dequeuedEnemy != enemy)
                    {
                        newQueue.Enqueue(dequeuedEnemy);
                    }
                }
                enemiesInRange = newQueue;
            }
        }
    }

    private IEnumerator AttackEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(teslaConfig.attackInterval);

            if (enemiesInRange.Count > 0)
            {
                EnemyControl enemy = enemiesInRange.Peek();
                if (enemy != null)
                {
                    enemy.TakeDamage(enemy.gameObject, teslaConfig.damage);
                }

                if (enemy == null || enemy.stats.health <= 0)
                {
                    enemiesInRange.Dequeue();
                }
            }
        }
    }
}
