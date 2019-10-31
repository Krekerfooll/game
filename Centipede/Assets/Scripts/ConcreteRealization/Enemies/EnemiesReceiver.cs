using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Can receive enemies damage
/// </summary>
public class EnemiesReceiver : DamageReceiver
{
    private void OnCollisionEnter(Collision collision)
    {
        EnemyElement enemy = collision.gameObject.GetComponent<EnemyElement>();

        if (enemy != null)
            SetDamage(enemy.Contact());
    }
}
