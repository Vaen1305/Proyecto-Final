using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernalTower : Tower
{
    public InfernalTowerConfig infernalTowerConfig;
    private float currentDamage;
    public LineRenderer lineRenderer;
    private GameObject currentTarget;
    void Start()
    {
        if (infernalTowerConfig == null)
        {
            return;
        }

        currentDamage = infernalTowerConfig.initialDamage;
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {

        }
        else
        {
            lineRenderer.positionCount = 2;
        }
    }

    void Update()
    {
        if (!isPlaced)
        {
            return;
        }

        if (target == null || target.GetComponent<EnemyControl>().stats.health <= 0)
        {
            target = FindTarget();
        }

        if (target != null)
        {
            if (target != currentTarget)
            {
                currentTarget = target;
                currentDamage = infernalTowerConfig.initialDamage;
            }
            Attack(target);
        }
        else
        {
            DisableLineRenderer();
        }
    }

    protected override void Attack(GameObject target)
    {
        currentDamage += infernalTowerConfig.damageIncreaseRate * Time.deltaTime;
        EnemyControl enemyControl = target.GetComponent<EnemyControl>();

        if (enemyControl != null)
        {
            int damageToApply = Mathf.RoundToInt(currentDamage * Time.deltaTime * 10); 
            enemyControl.TakeDamage(gameObject, damageToApply);
        }

        DrawLightning(transform.position, target.transform.position);
    }

    private void DrawLightning(Vector3 start, Vector3 end)
    {
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
            lineRenderer.enabled = true;
        }
    }

    private void DisableLineRenderer()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }
}
