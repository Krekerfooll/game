using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class, that represent enemy
/// </summary>
public class EnemyBase : MonoBehaviour
{
    public bool wasDestroyed { get; protected set; }

    public virtual void Activate()
    {
        gameObject.SetActive(true);
        wasDestroyed = false;
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
        wasDestroyed = true;
    }
}
