using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileConfig config;
    public Vector3 targetPosition;
    public GameObject targetEnemy;

    public delegate void OnHitEnemyHandler(GameObject enemy, Projectile projectile);
    public event OnHitEnemyHandler OnHitEnemy;

    void Start()
    {
        if (targetEnemy != null)
        {
            EnemyControl enemyControl = targetEnemy.GetComponent<EnemyControl>();
            if (enemyControl != null)
            {
                OnHitEnemy += enemyControl.TakeDamage;
            }
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, config.speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyControl enemyControl = other.gameObject.GetComponent<EnemyControl>();
        if (enemyControl != null)
        {
            enemyControl.TakeDamage(other.gameObject, this);
        }
    }
}

