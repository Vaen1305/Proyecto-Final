using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerConfig config;
    public bool isPlaced = false;
    protected GameObject target;
    protected float attackTimer = 0f;
    public Transform shootPoint;
    void Update()
    {
        if (!isPlaced)
        {
            return;
        }

        attackTimer += Time.deltaTime;
        if (attackTimer >= config.attackSpeed)
        {
            target = FindTarget();
            if (target != null)
            {
                Attack(target);
                attackTimer = 0f;
            }
        }
    }

    protected virtual void Attack(GameObject target)
    {
        if (config.projectilePrefab != null && shootPoint != null)
        {
            GameObject projectileObject = Instantiate(config.projectilePrefab, shootPoint.position, Quaternion.identity); 
            Projectile projectile = projectileObject.GetComponent<Projectile>();

            if (projectile != null)
            {
                projectile.targetEnemy = target;
                projectile.targetPosition = target.transform.position;
                projectile.config = config.projectilePrefab.GetComponent<Projectile>().config;
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
