using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TeslaTowerConfig", menuName = "TowerDefense/TeslaTowerConfig")]
public class TeslaTowerConfig : ScriptableObject
{
    public float attackRadius = 5f;
    public float attackInterval = 1f;
    public int damage = 10;
    public int price = 50;
}
