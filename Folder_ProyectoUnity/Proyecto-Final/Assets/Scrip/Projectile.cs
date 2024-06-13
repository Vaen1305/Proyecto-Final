using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileConfig config;
    public Vector3 targetPosition;
    public GameObject targetEnemy;

    public delegate void OnHitEnemyHandler(GameObject enemy, int damage);
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
            HandleHit();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetEnemy)
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        if (targetEnemy != null)
        {
            OnHitEnemy?.Invoke(targetEnemy, config.damage);
        }
        Destroy(gameObject);
    }
}

