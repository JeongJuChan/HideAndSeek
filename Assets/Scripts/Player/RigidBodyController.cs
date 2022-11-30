using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class RigidBodyController : BaseController
{
    [SerializeField] float _maxSlopeAngle = 30f;
    // TODO : 삭제 : 테스트용
    [SerializeField] bool isSlope;
    [SerializeField] float testAngle;
    
    Rigidbody _rigidbody;
    Vector3 _moveDirection;
    RaycastHit _slopeHit;
    
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
        //Gizmos.DrawSphere(checkGroundTransform.position, checkGroundRadius);
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
        float fallSpeed = _rigidbody.velocity.y;

        _moveDirection = new Vector3(horizontal, 0f, vertical);
        
        if (OnSlope())
        {
            if (_moveDirection.Equals(Vector3.zero))
            {
                fallSpeed = 0f;
            }
            _moveDirection = GetSlopeMoveDirection() * (moveSpeed * Time.deltaTime);
        }
        else
        {
            _moveDirection = _moveDirection.normalized * (moveSpeed * Time.deltaTime);
        }
        
        _moveDirection.y = fallSpeed;
        _rigidbody.velocity = _moveDirection;

        // _rigidbody.useGravity = !OnSlope();
    }

    protected override void Jump()
    {
        Vector3 jumpVector = _rigidbody.velocity;
        jumpVector.y = -Physics.gravity.y * jumpPower;
        _rigidbody.velocity = jumpVector;
        // _rigidbody.AddForce(Vector3.up * Mathf.Sqrt(-2 * Physics.gravity.y * jumpPower), ForceMode.VelocityChange);
    }

    #endregion

    #region Private Methods

    bool OnSlope()
    {
        Debug.DrawRay(transform.position, GetSlopeMoveDirection() * 3f, Color.blue);
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        Debug.Log($"_slopeHit.normal : {_slopeHit.normal}");
        //Debug.DrawRay(transform.position + _moveDirection.normalized * 0.05f + Vector3.up * transform.lossyScale.y * 0.5f, Vector3.down * transform.lossyScale.y * 0.5f, Color.red);
        isSlope = Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, out _slopeHit, 1f, groundLayer);
        testAngle = Vector3.Angle(Vector3.up, _slopeHit.normal);
        if (Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, out _slopeHit, 1f, groundLayer))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _maxSlopeAngle && angle != 0;
        }

        return false;
    }

    Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal).normalized;
    }

    #endregion
    
}
