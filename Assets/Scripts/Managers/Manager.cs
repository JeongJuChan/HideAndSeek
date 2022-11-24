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

    SceneManagerEx _scene = new SceneManagerEx();
    
    public static SceneManagerEx Scene { get { return Instance._scene; } }

    #endregion
    
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
