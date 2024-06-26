using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectProjectile : MonoBehaviour
{
    public AreaEffectProjectileConfig config;
    public Vector3 targetPosition;
    public GameObject targetEnemy;
    private Vector3 initialPosition;
    private float travelTime;
    private float elapsedTime;

    void Start()
    {
        initialPosition = transform.position;
        travelTime = (targetPosition - initialPosition).magnitude / config.speed;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        float t = elapsedTime / travelTime;
        if (t > 1f) t = 1f;

        float x = Mathf.Lerp(initialPosition.x, targetPosition.x, t);
        float y = Mathf.Lerp(initialPosition.y, targetPosition.y, t) - 0.5f * config.gravity * t * t;
        float z = Mathf.Lerp(initialPosition.z, targetPosition.z, t);

        transform.position = new Vector3(x, y, z);

        if (t >= 1f)
        {
            Explode();
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, config.explosionRadius);
        for (int i = 0; i < hitColliders.Length; ++i)
        {
            EnemyControl enemyControl = hitColliders[i].GetComponent<EnemyControl>();
            if (enemyControl != null)
            {
                enemyControl.TakeDamage(hitColliders[i].gameObject, config.damage);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == targetEnemy)
        {
            Explode();
            Destroy(gameObject);
        }
    }
}
