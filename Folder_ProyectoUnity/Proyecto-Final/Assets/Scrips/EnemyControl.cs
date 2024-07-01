using UnityEngine;
using System;

public class EnemyControl : MonoBehaviour
{
    public EnemyStats stats = new EnemyStats();
    private Graph graph;
    private Node currentNode;
    private int currentWaypointIndex = 0;
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

        graph = new Graph();
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
        switch (enemyType)
        {
            case "Type1":
                InitializeType1Graph();
                break;
            case "Type2":
                InitializeType2Graph();
                break;
            case "Type3":
                InitializeType3Graph();
                break;
            default:

                break;
        }
    }

    private void InitializeType1Graph()
    {
        Transform waypoint1 = GameObject.Find("Waypoint1").transform;
        Transform waypoint2 = GameObject.Find("Waypoint2").transform;
        Transform waypoint3 = GameObject.Find("Waypoint3").transform;
        Transform waypoint4 = GameObject.Find("Waypoint4").transform;
        Transform waypoint5 = GameObject.Find("Waypoint5").transform;
        Transform waypoint6 = GameObject.Find("Waypoint6").transform;
        Transform waypoint7 = GameObject.Find("Waypoint7").transform;

        Node node1 = new Node(waypoint1);
        Node node2 = new Node(waypoint2);
        Node node3 = new Node(waypoint3);
        Node node4 = new Node(waypoint4);
        Node node5 = new Node(waypoint5);
        Node node6 = new Node(waypoint6);
        Node node7 = new Node(waypoint7);

        node1.AddEdge(node2);
        node2.AddEdge(node3);
        node3.AddEdge(node4);
        node4.AddEdge(node5);
        node5.AddEdge(node6);
        node6.AddEdge(node7);

        graph.AddNode(node1);
        graph.AddNode(node2);
        graph.AddNode(node3);
        graph.AddNode(node4);
        graph.AddNode(node5);
        graph.AddNode(node6);
        graph.AddNode(node7);
    }

    private void InitializeType2Graph()
    {
        Transform waypoint8 = GameObject.Find("Waypoint8").transform;
        Transform waypoint9 = GameObject.Find("Waypoint9").transform;
        Transform waypoint10 = GameObject.Find("Waypoint10").transform;
        Transform waypoint11 = GameObject.Find("Waypoint11").transform;
        Transform waypoint12 = GameObject.Find("Waypoint12").transform;
        Transform waypoint13 = GameObject.Find("Waypoint13").transform;
        Transform waypoint14 = GameObject.Find("Waypoint14").transform;
        Transform waypoint15 = GameObject.Find("Waypoint15").transform;
        Transform waypoint16 = GameObject.Find("Waypoint16").transform;

        Node node8 = new Node(waypoint8);
        Node node9 = new Node(waypoint9);
        Node node10 = new Node(waypoint10);
        Node node11 = new Node(waypoint11);
        Node node12 = new Node(waypoint12);
        Node node13 = new Node(waypoint13);
        Node node14 = new Node(waypoint14);
        Node node15 = new Node(waypoint15);
        Node node16 = new Node(waypoint16);

        node8.AddEdge(node9);
        node9.AddEdge(node10);
        node10.AddEdge(node11);
        node11.AddEdge(node12);
        node12.AddEdge(node13);
        node13.AddEdge(node14);
        node14.AddEdge(node15);
        node15.AddEdge(node16);

        graph.AddNode(node8);
        graph.AddNode(node9);
        graph.AddNode(node10);
        graph.AddNode(node11);
        graph.AddNode(node12);
        graph.AddNode(node13);
        graph.AddNode(node14);
        graph.AddNode(node15);
        graph.AddNode(node16);
    }

    private void InitializeType3Graph()
    {
        Transform waypoint7 = GameObject.Find("Waypoint7").transform;
        Transform waypoint8 = GameObject.Find("Waypoint8").transform;
        Transform waypoint9 = GameObject.Find("Waypoint9").transform;

        Node node7 = new Node(waypoint7);
        Node node8 = new Node(waypoint8);
        Node node9 = new Node(waypoint9);

        node7.AddEdge(node8);
        node8.AddEdge(node9);

        graph.AddNode(node7);
        graph.AddNode(node8);
        graph.AddNode(node9);
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
