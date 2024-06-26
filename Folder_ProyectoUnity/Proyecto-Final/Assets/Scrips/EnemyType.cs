using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyType
{
    public GameObject enemyPrefab;
    public int count;
    public EnemyType(GameObject prefab, int count)
    {
        this.enemyPrefab = prefab;
        this.count = count;
    }
}
