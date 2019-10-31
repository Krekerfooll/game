using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// More complete class than BaseEnemy, can take damage and store enemy hit points
/// </summary>
public class Enemy : EnemyBase, IObserver
{
    public bool crash { get; private set; }
    public int damage;

    private HitPoints hitPoints;

    public DamageReceiver[] damageReceivers;

    public ScriptableHitPoints scriptableHitPoints;

    public BaseImplementer[] baseImplementers;

    private void Awake()
    {
        hitPoints = scriptableHitPoints.CreateHitPointsClass();

        foreach (DamageReceiver item in damageReceivers)
        {
            item.RegisterObserver(hitPoints);
        }


        if (baseImplementers.Length > 0)
        {
            foreach (BaseImplementer item in baseImplementers)
            {
                item.Calculate(hitPoints.GetHitPointsMax());
                hitPoints.RegisterObserver(item);
            }
        }

        hitPoints.RegisterObserver(this);
    }

    public int Crash()
    {
        crash = true;

        return damage;
    }

    public override void Activate()
    {
        base.Activate();
        hitPoints.ResetHitPoints();
    }

    public void UpdateState(ISubject s)
    {
        if ((int)s.GetData() == 0)
            Deactivate();
    }
}
