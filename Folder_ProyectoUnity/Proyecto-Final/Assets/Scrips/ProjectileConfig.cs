using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Config", menuName = "Tower Defense/Projectile Config")]
public class ProjectileConfig : ScriptableObject
{
    public float speed; 
    public int damage; 
}
