using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileConfig config;
    private Vector3 direction;

    public delegate void OnHitEnemyHandler(GameObject enemy, int damage);
    public static event OnHitEnemyHandler OnHitEnemy;

    void Update()
    {
        transform.position += direction * config.speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Limite"))
        {
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            HandleHit(other.gameObject);
        }
    }

    private void HandleHit(GameObject targetEnemy)
    {
        EnemyControl enemyControl = targetEnemy.GetComponent<EnemyControl>();
        if (enemyControl != null)
        {
            OnHitEnemy?.Invoke(targetEnemy, config.damage);
        }

        Destroy(gameObject);
    }

    public void Launch(Vector3 launchDirection)
    {
        direction = launchDirection.normalized;
    }
}
