using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CastleCollision : MonoBehaviour
{
    public static event Action<int> OnEnemyCollision;
    [SerializeField] private int damageToPlayer = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            OnEnemyCollision?.Invoke(damageToPlayer);
            Destroy(other.gameObject);
        }
    }
}
