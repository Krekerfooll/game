using System.Collections.Generic;

/// <summary>
/// Class, that can manage and store hit points
/// </summary>
[System.Serializable]
public class HitPoints : ISubject, IObserver
{
    private int hitPoints;
    private int maxHitPoints;

    public HitPoints(int hitPoints, int maxHitPoints)
    {
        this.hitPoints = hitPoints;
        this.maxHitPoints = maxHitPoints;
        CheckHitPoints();
    }


    public void SubHitPoints(int modifier)
    {
        hitPoints -= modifier;
        CheckHitPoints();

        NotifyObservers();
    }


    public void ResetHitPoints()
    {
        hitPoints = maxHitPoints;

        NotifyObservers();
    }

    public void ChangeHitPointsMax(int newMax)
    {
        maxHitPoints = newMax;
        CheckHitPoints();
    }

    private void CheckHitPoints()
    {
        if (hitPoints > maxHitPoints)
            hitPoints = maxHitPoints;
        if (hitPoints < 0)
            hitPoints = 0;
    }


    public int GetHitPoints()
    {
        return hitPoints;
    }

    public int GetHitPointsMax()
    {
        return maxHitPoints;
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
        return GetHitPoints();
    }


    public void UpdateState(ISubject s)
    {
        SubHitPoints((int)s.GetData());
    }

    #endregion
}
