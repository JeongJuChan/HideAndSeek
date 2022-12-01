using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChooser : MonoBehaviour
{
    [SerializeField] Transform _modelTransform;

    GameObject _currentModel;
    
    #region Unity Methods

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    

    #endregion

    #region Public Methods

    public void ChooseModel()
    {
        string[] types = Enum.GetNames(typeof(Define.ColliderType));
        int[] counts = new int[types.Length];
        for (int i = 0; i < types.Length; i++)
        {
            counts[i] = Manager.Pool.GetCounts(types[i]);
        }
        
    }

    #endregion

    #region Private Methods

    

    #endregion
}
