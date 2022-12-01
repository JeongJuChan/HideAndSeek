using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolManager
{
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;

    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        List<GameObject> _poolList = new List<GameObject>();

        public void Init(GameObject[] gameObjects, Transform root)
        {
            Original = root.gameObject;
            Root = root;
            _poolList.AddRange(gameObjects);
            foreach (GameObject go  in _poolList)
            {
                GameObject tempGo = Object.Instantiate(go, Vector3.zero, Quaternion.identity, Root);
                tempGo.SetActive(false);
            }
        }
        
        public void AddAndSetOff(GameObject go)
        {
            go.SetActive(false);
            _poolList.Add(go);
        }

        public GameObject RemoveAndSetOn(string name)
        {
            foreach (GameObject go in _poolList)
            {
                if (go.name.Equals(name))
                {
                    go.SetActive(true);
                    _poolList.Remove(go);
                    return go;
                }
            }
            return null;
        }

        public int GetCount()
        {
            return _poolList.Count;
        }
    }
    
    public void CreatePool()
    {
        string[] types = Enum.GetNames(typeof(Define.ColliderType));
        _root = new GameObject("Pool").transform;
        foreach (string pathName in types)
        {
            string path = $"Prefabs/Objects/{pathName}";
            _pool[pathName] = new Pool();
            
            Transform childTransform = new GameObject(pathName).transform;
            childTransform.parent = _root;
            _pool[pathName].Init(Manager.Resource.LoadAll<GameObject>(path), childTransform); 
        }
    }

    public void GetGameObject()
    {
        
        
    }

    public GameObject ReleaseGameObject(GameObject go)
    {
        return null;
    }

    public int GetCounts(string typeName)
    {
        return _pool[typeName].GetCount();
    }

    
}
