using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hard-coded class, that give right-left possibility to move
/// </summary>
public class MovingController : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField]
    private float speed;

    [SerializeField]
    private float leftBorder;

    [SerializeField]
    private float rightBorder;

    private float leftSecureBorder;
    private float rightSecureBorder;

    private void Awake()
    {
        speed *= 100;
        leftSecureBorder = leftBorder + ((leftBorder < 0) ? speed : -speed) * Time.deltaTime;
        rightSecureBorder = rightBorder + ((rightBorder < 0) ? speed : -speed) * Time.deltaTime;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > leftSecureBorder)
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < rightSecureBorder)
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
}
