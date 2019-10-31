using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public float cooldownInSeconds;
    private float lastTimeCheck;

    [SerializeField]
    private EnemiesDataBase dataBase;
    private ObjectPool enemiesPool;

    [Space]

    [SerializeField]
    private GridController grid;

    private List<GameObject> enemies;
    private List<string> enemiesTypes;

    private void Awake()
    {
        enemiesPool = new ObjectPool(dataBase);
        enemies = new List<GameObject>();
        enemiesTypes = new List<string>();

        lastTimeCheck = Time.realtimeSinceStartup;
    }

    private void Update()
    {
        if (!grid.GridIsFull())
        {
            if (CheckCooldownAvailability())
            {
                GameObject result;
                string enemiesType = dataBase.GetTypes()[Random.Range(0, dataBase.GetTypes().Length)];
                if (enemiesPool.Acquire(enemiesType, out result))
                {
                    result.GetComponent<EnemiesBase>().Activate();
                    grid.AddObjectToGrid(result);
                    enemies.Add(result);
                    enemiesTypes.Add(enemiesType);
                }
            }
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<EnemiesBase>().wasDestroyed)
            {
                grid.RemoveObjectFromGrid(enemies[i]);
                enemiesPool.Release(enemiesTypes[i], enemies[i]);
                enemies.Remove(enemies[i]);
                enemiesTypes.Remove(enemiesTypes[i]);
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
