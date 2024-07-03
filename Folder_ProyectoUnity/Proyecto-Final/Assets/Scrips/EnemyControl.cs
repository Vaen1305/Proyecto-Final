using UnityEngine;
using System;

public class EnemyControl : MonoBehaviour
{
    public EnemyStats stats = new EnemyStats();
    public WaypointPath waypointPath;
    private Graph graph;
    private Node currentNode;
    private float timeSinceStart = 0f;

    public AnimationCurve speedCurve;
    public string enemyType;

    public static event Action<int> OnEnemyDeath;

    void Start()
    {
        if (stats == null)
        {
            stats = new EnemyStats();
        }

        InitializeGraph();

        if (graph.Count > 0)
        {
            currentNode = graph.GetNode(0);
        }

        Projectile.OnHitEnemy += HandleHitByProjectile;
    }

    void OnDestroy()
    {
        Projectile.OnHitEnemy -= HandleHitByProjectile;
        WaveController.Instance.OnEnemyDestroyed();
    }

    void Update()
    {
        if (currentNode == null)
        {
            return;
        }

        Transform target = currentNode.Waypoint;

        timeSinceStart += Time.deltaTime;

        float adjustedSpeed = stats.speed * speedCurve.Evaluate(timeSinceStart);

        Vector3 direction = (target.position - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, adjustedSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            MoveToNextNode();
        }
    }

    private void MoveToNextNode()
    {
        if (currentNode.Edges.Count > 0)
        {
            currentNode = currentNode.Edges.Get(0);
        }
        else
        {
            currentNode = null;
        }
    }

    private void InitializeGraph()
    {
        graph = new Graph();

        for (int i = 0; i < waypointPath.waypoints.Length; i++)
        {
            Node node = new Node(waypointPath.waypoints[i]);
            graph.AddNode(node);

            if (i > 0)
            {
                Node previousNode = graph.GetNode(i - 1);
                previousNode.AddEdge(node);
            }
        }
    }

    private void HandleHitByProjectile(GameObject enemy, int damage)
    {
        if (enemy == this.gameObject)
        {
            TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        stats.health -= damage;
        if (stats.health <= 0)
        {
            Die();
            Destroy(gameObject);
        }
    }

    void Die()
    {
        OnEnemyDeath?.Invoke(stats.pointsOnDeath);
        GameManager.Instance.AddScore(stats.pointsOnDeath);
        GiveMoneyToPlayer(stats.pointsOnDeath);
    }

    void GiveMoneyToPlayer(int amount)
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.AddMoney(amount);
        }
    }
}
