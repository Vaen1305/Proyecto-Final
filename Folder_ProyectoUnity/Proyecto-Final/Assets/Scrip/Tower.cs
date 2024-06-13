using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerConfig config;
    public bool isPlaced = false;

    private float attackTimer = 0f;

    void Update()
    {
        if (!isPlaced)
        {
            return;
        }

        attackTimer += Time.deltaTime;
        if (attackTimer >= config.attackSpeed)
        {
            GameObject target = FindTarget();
            if (target != null)
            {
                Attack(target);
                attackTimer = 0f;
            }
        }
    }
    protected virtual void Attack(GameObject target)
    {

    }

    GameObject FindTarget()
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

