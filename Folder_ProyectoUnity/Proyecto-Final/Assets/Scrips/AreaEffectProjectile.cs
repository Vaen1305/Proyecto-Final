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

        initialPosition = transform.position;//O(1)
        Vector3 toTarget = targetPosition - initialPosition;//O(1)

        float horizontalDistance = new Vector2(toTarget.x, toTarget.z).magnitude;//O(1)
        float verticalDistance = toTarget.y;//O(1)
        float initialHeight = initialPosition.y;//O(1)

        float desiredElevationFactor = 1.5f;//O(1)
        float timeToReachTarget = horizontalDistance / config.speed;//O(1)
        float adjustedVerticalDistance = verticalDistance + (desiredElevationFactor * Mathf.Abs(verticalDistance));//O(1)
        float launchAngle = Mathf.Atan((adjustedVerticalDistance + 0.5f * config.gravity * timeToReachTarget * timeToReachTarget) / horizontalDistance);//O(1)
        float launchSpeedY = config.speed * Mathf.Sin(launchAngle);//O(1)
        float launchSpeedXZ = config.speed * Mathf.Cos(launchAngle);//O(1)

        velocity = new Vector3(toTarget.x / horizontalDistance * launchSpeedXZ, launchSpeedY, toTarget.z / horizontalDistance * launchSpeedXZ);//O(1)
    }//Tiempo Asintotico = O(1);

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
