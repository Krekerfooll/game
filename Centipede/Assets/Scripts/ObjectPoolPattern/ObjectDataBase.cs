using UnityEngine;

public interface IObjectDataBase
{
    string[] GetTypes();
    bool CreatePoolingObject(string type, out GameObject poolingObject);
}
