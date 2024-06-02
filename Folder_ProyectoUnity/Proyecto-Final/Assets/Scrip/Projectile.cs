using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileConfig config;
    public Vector3 targetPosition; 

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, config.speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }
}

