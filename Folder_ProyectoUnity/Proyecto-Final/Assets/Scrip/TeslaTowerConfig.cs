using UnityEngine;

[CreateAssetMenu(fileName = "TeslaTowerConfig", menuName = "TowerDefense/TeslaTowerConfig")]
public class TeslaTowerConfig : ScriptableObject
{
    public int damage;
    public float attackInterval;
    public float attackRadius;
    public GameObject lightningEffectPrefab; 
    public int price;
}
