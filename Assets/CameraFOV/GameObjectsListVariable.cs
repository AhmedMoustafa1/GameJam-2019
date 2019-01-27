using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectsListVariable", menuName = "ScriptableObjects/variables/arrays/GameObjectsListVariable")]
public class GameObjectsListVariable : ScriptableObject
{

    public List<GameObject> list = new List<GameObject>();


    public void Add(GameObject go)
    {
        if (!Has(go))
            list.Add(go);
    }

    internal bool Remove(GameObject go)
    {
        return list.Remove(go);
    }

    public GameObject Get(int index)
    {
        if (list.Count > index)
            return list[index];
        else
            return null;
    }

    public bool Has(GameObject go)
    {
        return list.IndexOf(go) > 0;
    }

    public bool IndexOf(GameObject go)
    {
        return list.IndexOf(go) > 0;
    }
}
