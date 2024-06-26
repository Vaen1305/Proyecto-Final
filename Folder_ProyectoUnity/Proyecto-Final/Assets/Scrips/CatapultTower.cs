using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultTower : Tower
{
    public AreaEffectProjectileConfig areaEffectProjectileConfig;

    protected override void Attack(GameObject target)
    {
        GameObject projectileObject = Instantiate(config.projectilePrefab, transform.position, Quaternion.identity);
        AreaEffectProjectile projectile = projectileObject.GetComponent<AreaEffectProjectile>();
        projectile.config = areaEffectProjectileConfig;
        projectile.targetPosition = target.transform.position;
        projectile.targetEnemy = target;
    }
}
    