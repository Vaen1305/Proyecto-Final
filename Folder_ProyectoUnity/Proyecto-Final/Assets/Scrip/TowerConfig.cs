using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Config", menuName = "Tower Defense/Tower Config")]
public class TowerConfig : ScriptableObject
{
    public float attackRange; 
    public float attackSpeed; 
    public GameObject projectilePrefab;
    public int price;
}

