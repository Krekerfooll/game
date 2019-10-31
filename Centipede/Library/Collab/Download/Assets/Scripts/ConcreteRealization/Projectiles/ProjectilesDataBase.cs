using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectilesDataBase", menuName = "DataBases/ProjectilesDataBase")]
public class ProjectilesDataBase : ScriptableObject, IObjectDataBase
{
    [SerializeField]
    private List<ScriptableProjectile> scriptableProgectiles;

    public string[] GetTypes()
    {
        string[] types = new string[scriptableProgectiles.Count];

        for (int i = 0; i < scriptableProgectiles.Count; i++)
        {
            types[i] = scriptableProgectiles[i].Type;
        }

        return types;
    }

    public bool CreatePoolingObject(string type, out GameObject result)
    {
        foreach (ScriptableProjectile item in scriptableProgectiles)
        {
            if (item.Type == type)
            {
                result = item.CreateProjectile();
                return true;
            }
        }

        result = null;
        return false;
    }
}
