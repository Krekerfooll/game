using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class, that can shoot projectiles from given database in different directions
/// </summary>
public class ShootingAction : MonoBehaviour
{
    public KeyCode ShootKey = KeyCode.Mouse0;
    public string projectileType;
    public float speedMultiplier = 1;

    public float cooldownInSeconds;
    private float lastTimeCheck;

    [SerializeField]
    private ProjectilesDataBase dataBase;
    private ObjectPool projectilesPool;

    List<GameObject> projectiles;

    private void Awake()
    {
        projectilesPool = new ObjectPool(dataBase);
        projectiles = new List<GameObject>();

        lastTimeCheck = Time.realtimeSinceStartup;
    }

    private void Update()
    {
        if (Input.GetKeyDown(ShootKey))
        {
            if (CheckCooldownAvailability())
            {
                GameObject result;
                if (projectilesPool.Acquire(projectileType, out result))
                {
                    result.GetComponent<Projectile>().Shoot(transform.position, Vector3.up, speedMultiplier);
                    projectiles.Add(result);
                }
            }
        }

        for (int i = 0; i < projectiles.Count; i++)
        {
            if (projectiles[i].GetComponent<Projectile>().hit)
            {
                projectilesPool.Release(projectileType, projectiles[i]);
                projectiles.Remove(projectiles[i]);
            }
        }
    }

    private bool CheckCooldownAvailability()
    {
        if (Time.realtimeSinceStartup - lastTimeCheck >= cooldownInSeconds)
        {
            lastTimeCheck = Time.realtimeSinceStartup;
            return true;
        }
    
        return false;
    }
}
