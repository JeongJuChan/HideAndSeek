using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidBodyController : BaseController
{
    Rigidbody _rigidbody;
    
    
    #region Unity Methods

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(checkGroundTransform.position, checkGroundRadius);
    }
    

    #endregion

    #region Public Methods

    

    #endregion

    #region Protected Methods

    protected override void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected override void Move()
    {
        isGrounded = Physics.CheckSphere(checkGroundTransform.position, checkGroundRadius, groundLayer,
            QueryTriggerInteraction.Ignore);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, _rigidbody.velocity.y, vertical).normalized * moveSpeed;
        _rigidbody.velocity = direction * Time.fixedDeltaTime;
    }

    protected override void Jump()
    {
        // _rigidbody.velocity += new Vector3(0f, -2 * Physics.gravity.y * jumpPower, 0f);
        _rigidbody.AddForce(Vector3.up * Mathf.Sqrt(-2 * Physics.gravity.y * jumpPower), ForceMode.VelocityChange);
    }

    #endregion

    #region Private Methods

    

    #endregion
    
}
