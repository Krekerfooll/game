using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawn controller, that control enemies spawn on transform points
/// </summary>
public class SpawnPointController : SpawnController
{
    [SerializeField]
    private List<Transform> spawnPoints;

    private List<Vector2> freePositions;
    private Dictionary<GameObject, Vector2> occupiedPositionGameObjects;


    private void Awake()
    {
        freePositions = new List<Vector2>();
        occupiedPositionGameObjects = new Dictionary<GameObject, Vector2>();

        foreach (Transform item in spawnPoints)
            freePositions.Add(item.position);
    }

    public override bool SpawnerIsFull()
    {
        return freePositions.Count == 0;
    }

    public override void AddObjectToSpawner(GameObject gameObject)
    {
        if (freePositions.Count != 0)
        {
            int index = Random.Range(0, freePositions.Count);
            Vector2 freePositionElement = freePositions[index];

            gameObject.transform.position = freePositionElement;

            freePositions.Remove(freePositionElement);
            occupiedPositionGameObjects.Add(gameObject, freePositionElement);
        }
    }

    public override void RemoveObjectFromSpawner(GameObject gameObject)
    {
        Vector2 occupiedPositionElement;
        if (occupiedPositionGameObjects.TryGetValue(gameObject, out occupiedPositionElement))
        {
            occupiedPositionGameObjects.Remove(gameObject);
            freePositions.Add(occupiedPositionElement);
        }
    }
}
