using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverComponent : DamageDealer
{
    [SerializeField]
    private int damage;

    public override int Contact()
    {
        return damage;
    }
}
