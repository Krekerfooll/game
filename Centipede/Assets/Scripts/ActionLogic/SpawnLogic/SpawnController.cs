using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base spawn controller
/// </summary>
public abstract class SpawnController : MonoBehaviour
{
    public int startEnemiesEmount;

    public abstract bool SpawnerIsFull();

    public abstract void AddObjectToSpawner(GameObject gameObject);

    public abstract void RemoveObjectFromSpawner(GameObject gameObject);
}
