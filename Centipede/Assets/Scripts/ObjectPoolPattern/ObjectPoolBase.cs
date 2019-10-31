using UnityEngine;

public interface IObjectPool
{
    bool Acquire(string type, out GameObject poolingObject);
    void Release(string type, GameObject poolingObject);
}
