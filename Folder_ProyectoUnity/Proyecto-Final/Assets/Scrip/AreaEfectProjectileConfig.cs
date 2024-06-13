using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Area Effect Projectile Config", menuName = "Tower Defense/Area Effect Projectile Config")]
public class AreaEffectProjectileConfig : ScriptableObject
{
    public float speed;
    public int damage;
    public float explosionRadius;
    public float gravity;
}
