using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElement : DamageDealer
{
    public bool crash { get; private set; }
    public int damage;

    public override int Contact()
    {
        crash = true;

        return damage;
    }

    public void ResetCrash()
    {
        crash = false;
    }
}
