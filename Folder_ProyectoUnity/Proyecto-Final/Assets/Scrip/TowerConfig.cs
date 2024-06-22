using UnityEngine;

[CreateAssetMenu(fileName = "TowerConfig", menuName = "TowerDefense/TowerConfig")]
public class TowerConfig : ScriptableObject
{
    public float attackRange;
    public float attackSpeed;
    public GameObject projectilePrefab;
    public int price;
}
