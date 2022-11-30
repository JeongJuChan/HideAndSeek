using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    #region Singletons
    
    static Manager _instance;

    static Manager Instance
    {
        get
        {
            if (!_instance)
            {
                GameObject go = new GameObject("@Manager");
                _instance = go.AddComponent<Manager>();
            }
            return _instance;
        }
    }


    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }

    #endregion
    
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
