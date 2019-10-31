using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour, IObjectPool
{
    private IObjectDataBase objectsDataBase;
    private Dictionary<string, List<GameObject>> freeObjects;
    private Dictionary<string, List<GameObject>> occupiedObjects;

    public ObjectPool(IObjectDataBase database)
    {
        objectsDataBase = database;
        freeObjects = new Dictionary<string, List<GameObject>>();
        occupiedObjects = new Dictionary<string, List<GameObject>>();

        foreach (string item in objectsDataBase.GetTypes())
        {
            freeObjects.Add(item, new List<GameObject>());
            occupiedObjects.Add(item, new List<GameObject>());
        }
    }

    public bool Acquire(string type, out GameObject result)
    {
        List<GameObject> current = new List<GameObject>();

        if (freeObjects.TryGetValue(type, out current))
        {
            for (int i = 0; i < current.Count; i++)
            {
                result = current[i];
                current.Remove(current[i]);
                if (occupiedObjects.TryGetValue(type, out current))
                    current.Add(result);
                return true;
            }

            if(occupiedObjects.TryGetValue(type, out current))
            {
                if (objectsDataBase.CreatePoolingObject(type, out result))
                {
                    current.Add(result);
                    return true;
                }
            }
        }

        result = null;
        return false;
    }

    public void Release(string type, GameObject gameObject)
    {
        List<GameObject> current = new List<GameObject>();

        if (occupiedObjects.TryGetValue(type, out current))
        {
            current.Remove(gameObject);
            if (freeObjects.TryGetValue(type, out current))
                current.Add(gameObject);
        }
    }
}
