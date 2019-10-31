using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Can receive damage from projectiles
/// </summary>
public class ProjectileReciever : DamageReceiver
{
    private void OnCollisionEnter(Collision collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();

        if (projectile != null)
            SetDamage(projectile.Contact());
    }
}
