using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesDataBase", menuName = "DataBases/EnemiesDataBase")]
public class EnemiesDataBase : ScriptableObject, IObjectDataBase
{
    [SerializeField]
    private List<ScriptableEnemies> scriptableEnemies;

    public string[] GetTypes()
    {
        string[] types = new string[scriptableEnemies.Count];

        for (int i = 0; i < scriptableEnemies.Count; i++)
        {
            types[i] = scriptableEnemies[i].Type;
        }

        return types;
    }

    public bool CreatePoolingObject(string type, out GameObject result)
    {
        foreach (ScriptableEnemies item in scriptableEnemies)
        {
            if (item.Type == type)
            {
                result = item.CreateEnemies();
                return true;
            }
        }

        result = null;
        return false;
    }
}