using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for different damage dealers
/// </summary>
public abstract class DamageDealer : MonoBehaviour
{
    public abstract int Contact();
}
