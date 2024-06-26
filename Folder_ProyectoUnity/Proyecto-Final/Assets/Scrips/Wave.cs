using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public SimplyLinkedList<EnemyType> enemies;

    public Wave(SimplyLinkedList<EnemyType> enemies)
    {
        this.enemies = enemies;
    }
}