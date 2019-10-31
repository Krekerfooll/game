using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridController : MonoBehaviour
{
    [SerializeField]
    private Vector2 leftDownPosition;
    [SerializeField]
    private Vector2 positionStep;
    [SerializeField]
    private Vector2Int gridSize = new Vector2Int();

    private List<Vector2> freePositionsGrid;
    private List<Vector2> occupiedPositionsGrid;

    private void Awake()
    {
        freePositionsGrid = new List<Vector2>();
        occupiedPositionsGrid = new List<Vector2>();

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                freePositionsGrid.Add(new Vector2(leftDownPosition.x + (positionStep.x * i), leftDownPosition.y + (positionStep.y * j)));
            }
        }
    }

    public bool GridIsFull()
    {
        return freePositionsGrid.Count == 0;
    }

    public void AddObjectToGrid(GameObject gameObject)
    {
        if (freePositionsGrid.Count != 0)
        {
            int index = Random.Range(0, freePositionsGrid.Count);
            Vector2 freePositionElement = freePositionsGrid[index];

            gameObject.transform.position = freePositionElement;

            freePositionsGrid.Remove(freePositionElement);
            occupiedPositionsGrid.Add(freePositionElement);
        }
    }

    public void RemoveObjectFromGrid(GameObject gameObject)
    {
        if (occupiedPositionsGrid.Contains(gameObject.transform.position)) {
            int index = occupiedPositionsGrid.IndexOf(gameObject.transform.position);
            Vector2 occupiedPositionElement = occupiedPositionsGrid[index];

            occupiedPositionsGrid.Remove(occupiedPositionElement);
            freePositionsGrid.Add(occupiedPositionElement);
        }
    }
}
