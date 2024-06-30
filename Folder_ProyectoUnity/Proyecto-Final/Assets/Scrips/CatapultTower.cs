using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultTower : Tower
{
    public AreaEffectProjectileConfig areaEffectProjectileConfig;

    void Start()
    {
        if (areaEffectProjectileConfig == null)
        {
            return;
        }
    }

    protected override void Attack(GameObject target)
    {
        if (config.projectilePrefab == null)
        {
            return;
        }

        if (shootPoint == null)
        {
            return;
        }

        GameObject projectileObject = Instantiate(config.projectilePrefab, shootPoint.position, shootPoint.rotation);
        AreaEffectProjectile projectile = projectileObject.GetComponent<AreaEffectProjectile>();

        if (projectile != null)
        {
            projectile.config = areaEffectProjectileConfig;
            projectile.targetPosition = target.transform.position;
        }
    }
}
