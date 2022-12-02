using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerChooser : MonoBehaviour
{
    [SerializeField] Transform _modelTransform;
    [SerializeField] int[] _counts;
    
    GameObject _currentModel;
    
    #region Unity Methods

    void Start()
    {
        SetModel();
    }

    void Update()
    {
        
    }
    

    #endregion

    #region Public Methods

    public void SetModel()
    {
        string[] types = Enum.GetNames(typeof(Define.ColliderType));
        _counts = new int[types.Length];
        for (int i = 0; i < types.Length; i++)
        {
            _counts[i] = Manager.Pool.GetCounts(types[i]);
        }
    }

    public void StopRoulette()
    {
    }

    #endregion

    #region Private Methods

    

    #endregion
}
