using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    Dictionary<string, Stack<GameObject>> _pool = new Dictionary<string, Stack<GameObject>>();
    Transform _root;
    
    public void CreatePool()
    {
        string[] types = Enum.GetNames(typeof(Define.ColliderType));
        foreach (string pathName in types)
        {
            string path = $"Prefabs/Objects/{pathName}";
            _pool[pathName] = new Stack<GameObject>(Manager.Resource.LoadAll<GameObject>(path));
            foreach (GameObject go in _pool[pathName])
            {
                _root = new GameObject("Pool").transform;
                
                
            }
        }
    }
}
