using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    void Awake()
    {
        Manager.Pool.CreatePool();
    }

    void Update()
    {
        
    }
}
