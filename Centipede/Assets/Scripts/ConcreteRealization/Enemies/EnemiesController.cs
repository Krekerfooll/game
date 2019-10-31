using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Can control enemies spawn, store and destroy
/// </summary>
public class EnemiesController : MonoBehaviour
{
    public float cooldownInSeconds;
    private float lastTimeCheck;

    [SerializeField]
    private EnemiesDataBase dataBase;
    private ObjectPool enemiesPool;

    [Space]

    [SerializeField]
    private SpawnController spawnController;

    private List<GameObject> enemies;
    private List<string> enemiesTypes;

    private void Start()
    {
        enemiesPool = new ObjectPool(dataBase);
        enemies = new List<GameObject>();
        enemiesTypes = new List<string>();

        lastTimeCheck = Time.realtimeSinceStartup;

        for (int i = 0; i < spawnController.startEnemiesEmount; i++)
        {
            if (!spawnController.SpawnerIsFull())
                SpawnNewEnemy();
        }
    }

    private void Update()
    {
        if (!spawnController.SpawnerIsFull())
        {
            if (CheckCooldownAvailability())
            {
                SpawnNewEnemy();
            }
        }

        RemoveDestroyedEnemies();
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

    private void SpawnNewEnemy()
    {
        GameObject result;
        string enemiesType = dataBase.GetTypes()[Random.Range(0, dataBase.GetTypes().Length)];
        if (enemiesPool.Acquire(enemiesType, out result))
        {
            result.GetComponent<EnemyBase>().Activate();
            spawnController.AddObjectToSpawner(result);
            enemies.Add(result);
            enemiesTypes.Add(enemiesType);
        }
    }

    private void RemoveDestroyedEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<EnemyBase>().wasDestroyed)
            {
                spawnController.RemoveObjectFromSpawner(enemies[i]);
                enemiesPool.Release(enemiesTypes[i], enemies[i]);
                enemies.Remove(enemies[i]);
                enemiesTypes.Remove(enemiesTypes[i]);
            }
        }
    }
}
