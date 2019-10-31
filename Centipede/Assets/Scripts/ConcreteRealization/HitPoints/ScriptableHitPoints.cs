using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HitPoints", menuName = "DataStructures/HitPoints")]
public class ScriptableHitPoints : ScriptableObject
{
    [SerializeField]
    private int hitPoints;
    [SerializeField]
    private int maxHitPoints;

    public HitPoints CreateHitPointsClass()
    {
        return new HitPoints(hitPoints, maxHitPoints);
    }
}
