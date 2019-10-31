using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(fileName = "Enemie", menuName = "ScriptableObjects/Enemie")]
public class ScriptableEnemies : ScriptableObject
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
    private GameObject enemies;


    public GameObject CreateEnemies()
    {
        GameObject enemies = Instantiate(this.enemies, Vector3.zero, Quaternion.identity) as GameObject;

        return enemies;
    }
}
