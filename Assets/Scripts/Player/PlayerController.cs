using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : BaseController
{
    CharacterController _characterController;
    Vector3 _VerticalVelocity;
    Vector3 _jumpVector;
    
    #region Unity Methods

    void Update()
    {
        Move();
        UpdateGravity();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
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
        _characterController = GetComponent<CharacterController>();
    }
    
    protected override void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized * moveSpeed;
        _characterController.Move(direction * Time.deltaTime);
    }

    protected override void Jump()
    {
        _VerticalVelocity.y += Mathf.Sqrt(-2 * Physics.gravity.y * jumpPower);
    }

    #endregion

    #region Private Methods

    void UpdateGravity()
    {
        isGrounded = Physics.CheckSphere(checkGroundTransform.position, checkGroundRadius, groundLayer,
            QueryTriggerInteraction.Ignore);
        if (isGrounded && _VerticalVelocity.y < 0)
            _VerticalVelocity.y = 0f;

        _VerticalVelocity.y += Physics.gravity.y * Time.deltaTime;
        _characterController.Move(_VerticalVelocity * Time.deltaTime);
    }
    
    
    #endregion

    
}
