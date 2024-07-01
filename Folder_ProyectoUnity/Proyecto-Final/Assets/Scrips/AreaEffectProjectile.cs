using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectProjectile : MonoBehaviour
{
    public AreaEffectProjectileConfig config;
    public Vector3 targetPosition;
    private Vector3 initialPosition;
    private Vector3 velocity;
    private float elapsedTime;

    void Start()
    {
        if (config == null)
        {
            return;
        }

        initialPosition = transform.position;
        Vector3 toTarget = targetPosition - initialPosition;

        float horizontalDistance = new Vector2(toTarget.x, toTarget.z).magnitude;
        float verticalDistance = toTarget.y;
        float initialHeight = initialPosition.y;

        float desiredElevationFactor = 1.5f;
        float timeToReachTarget = horizontalDistance / config.speed;
        float adjustedVerticalDistance = verticalDistance + (desiredElevationFactor * Mathf.Abs(verticalDistance));
        float launchAngle = Mathf.Atan((adjustedVerticalDistance + 0.5f * config.gravity * timeToReachTarget * timeToReachTarget) / horizontalDistance);
        float launchSpeedY = config.speed * Mathf.Sin(launchAngle);
        float launchSpeedXZ = config.speed * Mathf.Cos(launchAngle);

        velocity = new Vector3(toTarget.x / horizontalDistance * launchSpeedXZ, launchSpeedY, toTarget.z / horizontalDistance * launchSpeedXZ);

    }

    void Update()
    {
        if (config == null)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        Vector3 displacement = velocity * elapsedTime;
        displacement.y -= 0.5f * config.gravity * elapsedTime * elapsedTime;
        Vector3 newPosition = initialPosition + displacement;

        transform.position = newPosition;

        if (transform.position.y <= 0.5f) 
        {
            Explode();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Limite"))
        {
            Destroy(gameObject);
            return;
        }
        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, config.explosionRadius);
        for (int i = 0; i < hitColliders.Length; ++i)
        {
            EnemyControl enemyControl = hitColliders[i].GetComponent<EnemyControl>();
            if (enemyControl != null)
            {
                enemyControl.TakeDamage(config.damage);
            }
        }
    }
}
