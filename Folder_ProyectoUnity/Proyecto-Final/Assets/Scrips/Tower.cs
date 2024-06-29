using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerConfig config;
    public bool isPlaced = false;
    protected GameObject target;
    protected float attackTimer = 0f;
    [SerializeField] protected Transform shootPoint;

    void Update()
    {
        if (!isPlaced)
        {
            return;
        }

        target = FindTarget();
        if (target != null)
        {
            RotateTowardsTarget(target);

            attackTimer += Time.deltaTime;
            if (attackTimer >= config.attackSpeed)
            {
                Attack(target);
                attackTimer = 0f;
            }
        }
    }

    private void RotateTowardsTarget(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * config.rotationSpeed);
    }

    protected virtual void Attack(GameObject target)
    {
        if (config.projectilePrefab != null && shootPoint != null)
        {
            GameObject projectileObject = Instantiate(config.projectilePrefab, shootPoint.position, shootPoint.rotation);
            Projectile projectile = projectileObject.GetComponent<Projectile>();

            if (projectile != null)
            {
                Vector3 direction = (target.transform.position - shootPoint.position).normalized;
                projectile.Launch(direction);
            }
        }
    }

    protected GameObject FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < enemies.Length; ++i)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (distanceToEnemy < closestDistance && distanceToEnemy <= config.attackRange)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemies[i];
            }
        }

        return closestEnemy;
    }
}
