using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawn controller, that control enemies spawn on grid transform positions
/// </summary>
[System.Serializable]
public class GridController : SpawnController
{
    [SerializeField]
    private Vector2 leftDownPosition;
    [SerializeField]
    private Vector2 positionStep;
    [SerializeField]
    private Vector2Int gridSize = new Vector2Int();

    private List<Vector2> freePositions;
    private List<Vector2> occupiedPositions;

    private void Awake()
    {
        freePositions = new List<Vector2>();
        occupiedPositions = new List<Vector2>();

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                freePositions.Add(new Vector2(leftDownPosition.x + (positionStep.x * i), leftDownPosition.y + (positionStep.y * j)));
            }
        }
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
            occupiedPositions.Add(freePositionElement);
        }
    }

    public override void RemoveObjectFromSpawner(GameObject gameObject)
    {
        if (occupiedPositions.Contains(gameObject.transform.position)) {
            int index = occupiedPositions.IndexOf(gameObject.transform.position);
            Vector2 occupiedPositionElement = occupiedPositions[index];

            occupiedPositions.Remove(occupiedPositionElement);
            freePositions.Add(occupiedPositionElement);
        }
    }
}
