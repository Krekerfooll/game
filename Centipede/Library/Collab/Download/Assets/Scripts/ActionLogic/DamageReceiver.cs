using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour, ISubject
{
    private int damage;

    public void SetDamage(int damage)
    {
        this.damage = damage;
        NotifyObservers();
    }

    #region Observer pattern implementation

    private List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver o)
    {
        if (!observers.Contains(o))
        {
            observers.Add(o);
        }
    }

    public void RemoveObserver(IObserver o)
    {
        if (observers.Contains(o))
        {
            observers.Remove(o);
        }
    }

    public void NotifyObservers()
    {
        foreach (IObserver item in observers)
        {
            item.UpdateState(this);
        }
    }

    public object GetData()
    {
        return damage;
    }

    #endregion
}
