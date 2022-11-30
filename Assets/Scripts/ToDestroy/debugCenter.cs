using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugCenter : MonoBehaviour
{
    MeshRenderer _meshRenderer;
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        
    }

    void Update()
    {
        Debug.DrawRay(_meshRenderer.bounds.center + new Vector3(0f , 0f, _meshRenderer.localBounds.size.z / 2), Vector3.down, Color.yellow);
    }
}
