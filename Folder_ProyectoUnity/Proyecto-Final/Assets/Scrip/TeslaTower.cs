using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaTower : Tower
{
    public TeslaTowerConfig teslaConfig; // Referencia al scriptable object específico de TeslaTower
    private SortedSet<EnemyControl> enemiesInRange = new SortedSet<EnemyControl>(new EnemyComparer());
    private SphereCollider attackRange;

    void Start()
    {
        if (teslaConfig == null)
        {
            Debug.LogError("TeslaTowerConfig is not assigned!");
            return;
        }

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
            if (enemy != null)
            {
                enemiesInRange.Add(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyControl enemy = other.GetComponent<EnemyControl>();
            if (enemy != null)
            {
                enemiesInRange.Remove(enemy);
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
                foreach (EnemyControl enemy in enemiesInRange)
                {
                    if (enemy != null)
                    {
                        // Crear y configurar el efecto de rayo
                        GameObject lightningEffect = Instantiate(teslaConfig.lightningEffectPrefab, transform.position, Quaternion.identity);
                        LineRenderer lineRenderer = lightningEffect.GetComponent<LineRenderer>();
                        lineRenderer.SetPosition(0, transform.position);
                        lineRenderer.SetPosition(1, enemy.transform.position);

                        // Aplicar daño al enemigo
                        enemy.TakeDamage(enemy.gameObject, teslaConfig.damage);

                        // Destruir el efecto de rayo después de un corto tiempo
                        Destroy(lightningEffect, 0.2f);
                    }

                    if (enemy == null || enemy.stats.health <= 0)
                    {
                        enemiesInRange.Remove(enemy);
                        break;
                    }
                }
            }
        }
    }
}

public class EnemyComparer : IComparer<EnemyControl>
{
    public int Compare(EnemyControl x, EnemyControl y)
    {
        if (x == null || y == null)
        {
            return 0;
        }
        return x.stats.health.CompareTo(y.stats.health);
    }
}
    
