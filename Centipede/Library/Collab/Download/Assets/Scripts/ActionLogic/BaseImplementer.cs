using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseImplementer : MonoBehaviour, IObserver
{
    public abstract void Calculate(int data);

    protected abstract void ImplementData(int data);

    public void UpdateState(ISubject s)
    {
        ImplementData((int)s.GetData());
    }
}
