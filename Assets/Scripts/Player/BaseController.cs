using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpPower = 2f;
    [SerializeField] protected float checkGroundRadius = 1f;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Transform checkGroundTransform;
    
    protected bool isGrounded;

    void Start()
    {
        Init();
    }

    protected abstract void Init();
    protected abstract void Move();
    protected abstract void Jump();
}
