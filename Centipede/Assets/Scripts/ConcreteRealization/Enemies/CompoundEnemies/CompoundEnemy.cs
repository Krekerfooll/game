using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompoundEnemy : EnemyBase
{
    public float speed;

    public float levelHeight;
    private float headRotationSpeed;
    private float lastHeightPosition;

    public enum Direction { Left, Right, DownLeft, DownRight}
    private Direction direction = Direction.Right;


    public Vector3 elementPositionStep;
    public int startElementsAmount;


    private EnemyElement mainEnemyElement;

    public List<GameObject> mainElementVariety;
    public List<GameObject> elementsVariety;

    private GameObject mainElement;
    private List<GameObject> elements;


    private void Awake()
    {
        CreateCompoundEnemy();

        // Pi / (Time, that the enemy spends on the descent to one level)
        headRotationSpeed = Mathf.PI / (levelHeight / (speed * Time.deltaTime));
    }

    private void Start()
    {
        lastHeightPosition = mainElement.transform.position.y;
    }

    private void Update()
    {
        EnemyMovingLogic();

        DestroyedElementsCheck();
    }


    private void CreateCompoundEnemy()
    {
        if (mainElementVariety.Count > 0)
        {
            mainElement = Instantiate(mainElementVariety[Random.Range(0, mainElementVariety.Count)], transform.position, Quaternion.identity) as GameObject;
            mainElement.transform.SetParent(transform);
            mainEnemyElement = mainElement.GetComponent<EnemyElement>();

            if (elementsVariety.Count > 0)
            {
                elements = new List<GameObject>();

                for (int i = 0; i < startElementsAmount; i++)
                {
                    AddElement(elementsVariety[Random.Range(0, mainElementVariety.Count)]);
                }
            }
        }
    }

    public void AddElement(GameObject element)
    {
        if (mainElement != null && elements != null)
        {
            if (elements.Count == 0)
            {
                GameObject newElement = Instantiate(element, transform.position + elementPositionStep, Quaternion.identity) as GameObject;
                newElement.transform.SetParent(transform);
                elements.Add(newElement);
            }
            else
            {
                GameObject newElement = Instantiate(element, elements[elements.Count - 1].transform.position + elementPositionStep, Quaternion.identity) as GameObject;
                newElement.transform.SetParent(transform);
                elements.Add(newElement);
            }
        }
    }

    private void DestroyedElementsCheck()
    {
        // if main element was destroyed
        if (mainElement.GetComponent<EnemyBase>() != null)
        {
            if (mainElement.GetComponent<EnemyBase>().wasDestroyed)
            {
                if (elements.Count > 0)
                {
                    mainElement.GetComponent<EnemyBase>().Activate();
                    GameObject element = elements[0];
                    mainElement.transform.position = element.transform.position;
                    elements.Remove(element);
                    Destroy(element);
                }
                else
                {
                    Destroy(mainElement);
                    CreateCompoundEnemy();
                    lastHeightPosition = mainElement.transform.position.y;
                    GetComponent<EnemyBase>().Deactivate();
                }
            }
        }

        // if other elements were destroyed
        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].GetComponent<EnemyBase>() != null)
            {
                if (elements[i].GetComponent<EnemyBase>().wasDestroyed)
                {
                    GameObject element = elements[i];
                    elements.Remove(element);
                    Destroy(element);
                    i--;
                }
            }
        }
    }



    private void EnemyMovingLogic()
    {
        // this method has to be abstract in future
        // Translate() and Rotate() methods may only exist in child class
        // all, what this class has to doing, is a Compound Enemy creation and calling EnemyMovingLogic() in Update()
        // but when the project will be able to check, this class may remain in the incorrect form

        if (mainEnemyElement.crash)
        {
            switch (direction)
            {
                case Direction.Left:
                    direction = Direction.DownRight;
                    break;

                case Direction.Right:
                    direction = Direction.DownLeft;
                    break;
            }

            mainEnemyElement.ResetCrash();
        }

        switch (direction)
        {
            case Direction.Left:
                TranslateEnemy(Vector3.left);
                break;

            case Direction.Right:
                TranslateEnemy(Vector3.right);
                break;

            case Direction.DownLeft:
                if (Mathf.Abs(lastHeightPosition - mainElement.transform.position.y) < levelHeight)
                    TranslateEnemy(Vector3.down);
                else
                {
                    direction = Direction.Left;
                    lastHeightPosition -= levelHeight;
                }
                break;

            case Direction.DownRight:
                if (Mathf.Abs(lastHeightPosition - mainElement.transform.position.y) < levelHeight)
                    TranslateEnemy(Vector3.down);
                else
                {
                    direction = Direction.Right;
                    lastHeightPosition -= levelHeight;
                }
                break;
        }

        RotateEnemy();
    }

    private void TranslateEnemy(Vector3 vectorDir)
    {
        // every element just strive to reach the position of previous element by Lerp()
        if (elements.Count > 0)
        {
            for (int i = elements.Count - 1; i > 0; i--)
            {
                elements[i].transform.position = Vector3.Lerp(elements[i].transform.position, elements[i - 1].transform.position, speed * Time.deltaTime);
            }
            elements[0].transform.position = Vector3.Lerp(elements[0].transform.position, mainElement.transform.position, speed * Time.deltaTime);
        }
        // main element change their position with set speed by direction
        mainElement.transform.position += vectorDir * speed * Time.deltaTime;
    }

    private void RotateEnemy()
    {
        // every element just strive to reach the rotation of previous element by Lerp()
        if (elements.Count > 0)
        {
            for (int i = elements.Count - 1; i > 0; i--)
            {
                elements[i].transform.rotation = Quaternion.Lerp(elements[i].transform.rotation, elements[i - 1].transform.rotation, i * Time.deltaTime);
            }
            elements[0].transform.rotation = Quaternion.Lerp(elements[0].transform.rotation, mainElement.transform.rotation, 0.5f * Time.deltaTime);
        }

        // main element rotate around Z with headRotationSpeed when descending to one level
        switch (direction)
        {
            case Direction.DownLeft:
            case Direction.Left:
                mainElement.transform.rotation = Quaternion.Lerp(mainElement.transform.rotation, Quaternion.Euler(
                    mainElement.transform.rotation.x,
                    mainElement.transform.rotation.y,
                    mainElement.transform.rotation.z + 180),
                    headRotationSpeed); 
                break;

            case Direction.DownRight:
            case Direction.Right:
                mainElement.transform.rotation = Quaternion.Lerp(mainElement.transform.rotation, Quaternion.Euler(
                    mainElement.transform.rotation.x,
                    mainElement.transform.rotation.y,
                    mainElement.transform.rotation.z),
                    headRotationSpeed);
                break;
        }
    }
}
