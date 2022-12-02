using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] int _slotCount;
    [SerializeField] GameObject slotPanel;
    [SerializeField] GameObject[] _slots;

    void Awake()
    {
        SetSlot();
    }

    void SetSlot()
    {
        _slots = new GameObject[_slotCount];
        for (int i = 0; i < _slotCount; i++)
        {
            _slots[i] = Manager.Resource.Instantiate($"Prefabs/UI/Slot", slotPanel.transform);
        }
    }

    void Update()
    {
        
    }
}
