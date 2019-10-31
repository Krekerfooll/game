using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBase : MonoBehaviour, IObserver
{
    public bool wasDestroyed { get; private set; }

    private HitPoints hitPoints;

    //private int baseScorePoints;

    [SerializeField]
    private DamageReceiver damageReceiver;

    [SerializeField]
    private ScriptableHitPoints scriptableHitPoints;

    private BaseImplementer[] baseImplementers;

    private void Awake()
    {
        hitPoints = scriptableHitPoints.CreateHitPointsClass();

        damageReceiver.RegisterObserver(hitPoints);

        baseImplementers = GetComponents<BaseImplementer>();

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

    //public int GetScorePoints()
    //{
    //    return baseScorePoints;
    //}

    public void Activate()
    {
        gameObject.SetActive(true);
        hitPoints.ResetHitPoints();
        wasDestroyed = false;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        wasDestroyed = true;
    }

    public void UpdateState(ISubject s)
    {
        if ((int)s.GetData() == 0)
            Deactivate();
    }
}
