using System.Collections.Generic;
using UnityEngine;

public class TeslaTower : Tower
{
    public TeslaTowerConfig teslaTowerConfig;
    public LineRenderer lineRenderer;
    private List<EnemyControl> enemiesInRange = new List<EnemyControl>();
    private float attackCooldown;
    private float timeSinceLastAttack;

    void Start()
    {
        if (teslaTowerConfig == null)
        {
            return;
        }

        attackCooldown = teslaTowerConfig.attackCooldown;
        timeSinceLastAttack = attackCooldown;

        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {

        }
        else
        {
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }

        SphereCollider detectionCollider = gameObject.AddComponent<SphereCollider>();
        detectionCollider.isTrigger = true;
        detectionCollider.radius = teslaTowerConfig.attackRange;
    }

    void Update()
    {
        if (!isPlaced)
        {
            return;
        }

        timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack >= attackCooldown)
        {
            if (enemiesInRange.Count > 0)
            {
                AttackAllEnemies();
                timeSinceLastAttack = 0f;
            }
            else
            {
                DisableLineRenderer();
            }
        }
    }

    private void AttackAllEnemies()
    {
        List<Vector3> positions = new List<Vector3> { transform.position };

        foreach (EnemyControl enemyControl in enemiesInRange)
        {
            if (enemyControl != null && enemyControl.stats.health > 0)
            {
                Attack(enemyControl.gameObject);
                positions.Add(enemyControl.transform.position);
            }
        }

        DrawLightning(positions);
    }

    protected override void Attack(GameObject target)
    {
        EnemyControl enemyControl = target.GetComponent<EnemyControl>();

        if (enemyControl != null)
        {
            int damageToApply = Mathf.RoundToInt(teslaTowerConfig.fixedDamage);

            enemyControl.TakeDamage(gameObject, damageToApply);
        }
    }

    private void DrawLightning(List<Vector3> positions)
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positions.ToArray());
            lineRenderer.enabled = true;

            Invoke("DisableLineRenderer", 0.1f);
        }
    }

    private void DisableLineRenderer()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyControl enemyControl = other.GetComponent<EnemyControl>();
            if (enemyControl != null)
            {
                enemiesInRange.Add(enemyControl);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyControl enemyControl = other.GetComponent<EnemyControl>();
            if (enemyControl != null)
            {
                enemiesInRange.Remove(enemyControl);
            }
        }
    }
}
