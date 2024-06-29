using UnityEngine;

public class TeslaTower : Tower
{
    public TeslaTowerConfig teslaTowerConfig;
    public LineRenderer lineRenderer;
    private PriorityQueue<EnemyControl> enemiesInRange = new PriorityQueue<EnemyControl>();
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
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

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
        SimplyLinkedList<Vector3> positions = new SimplyLinkedList<Vector3>();
        positions.InsertNodeAtEnd(transform.position);

        int count = enemiesInRange.Count;
        for (int i = 0; i < count; i++)
        {
            EnemyControl enemyControl = enemiesInRange.Dequeue();
            if (enemyControl != null && enemyControl.stats.health > 0)
            {
                Attack(enemyControl.gameObject);
                positions.InsertNodeAtEnd(enemyControl.transform.position);
                enemiesInRange.Enqueue(enemyControl, enemyControl.stats.health);
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

    private void DrawLightning(SimplyLinkedList<Vector3> positions)
    {
        if (lineRenderer != null)
        {
            Vector3[] positionArray = new Vector3[positions.Count];
            for (int i = 0; i < positions.Count; i++)
            {
                positionArray[i] = positions.Get(i);
            }
            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positionArray);
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
            if (enemyControl != null && !enemiesInRange.Contains(enemyControl))
            {
                int priority = enemyControl.stats.health;
                enemiesInRange.Enqueue(enemyControl, priority);
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
