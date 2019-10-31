using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileReciever : DamageReceiver
{
    private void OnCollisionEnter(Collision collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();

        if (projectile != null)
            SetDamage(projectile.Hit());
    }
}
