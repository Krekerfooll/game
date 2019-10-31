using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Projectile")]
public class ScriptableProjectile : ScriptableObject
{
    [SerializeField]
    private string type;
    public string Type
    {
        get
        {
            return type;
        }
    }

    [SerializeField]
    private GameObject projectile;
    public GameObject Projectile
    {
        get
        {
            return projectile;
        }
    }

    [SerializeField]
    private Color color;
    public Color Color
    {
        get
        {
            return color;
        }
    }

    [SerializeField]
    private int baseDamage;
    public int BaseDamage
    {
        get
        {
            return baseDamage;
        }
    }

    [SerializeField]
    private float baseSpeed;
    public float BaseSpeed
    {
        get
        {
            return baseSpeed;
        }
    }

    public GameObject CreateProjectile()
    {
        GameObject projectile = Instantiate(this.projectile, Vector3.zero, Quaternion.identity) as GameObject;

        projectile.AddComponent<Projectile>().SetParams(color, baseSpeed, baseDamage);

        return projectile;
    }
}
