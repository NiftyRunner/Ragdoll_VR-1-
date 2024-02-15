using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ContinuousMovementPhysics : MonoBehaviour
{
    public float speed = 1f;
    public float turnSpeed = 60f;
    public InputActionProperty moveInputSource;
    public InputActionProperty turnInputSource;
    public Rigidbody rb;
    public LayerMask groundLayer;
    public CapsuleCollider bodyCollider;
    public Transform turnSource;

    public Transform directionSource;

    private Vector2 inputMoveAxis;
    private float inputTurnAxis;

    void Update()
    {
        inputMoveAxis = moveInputSource.action.ReadValue<Vector2>();
        inputTurnAxis = turnInputSource.action.ReadValue<Vector2>().x;
    }

    void FixedUpdate() 
    {
        //Quaternion yaw = Quaternion.Euler(0, directionSource.eulerAngles.y, 0);
        //Vector3 direction = yaw * new Vector3(inputMoveAxis.x, 0, inputMoveAxis.y);

        //rb.MovePosition(rb.position + direction * Time.fixedDeltaTime * speed);
        
        bool isGrounded = checkIfGrounded();

        if(isGrounded)
        {
            Quaternion yaw = Quaternion.Euler(0, directionSource.eulerAngles.y, 0);
            Vector3 direction = yaw * new Vector3(inputMoveAxis.x, 0, inputMoveAxis.y);

            //rb.MovePosition(rb.position + direction * Time.fixedDeltaTime * speed);
            
            Vector3 tragetMovePosition = rb.position + direction * Time.fixedDeltaTime * speed;

            Vector3 axis = Vector3.up;
            float angle = turnSpeed * Time.fixedDeltaTime * inputTurnAxis;

            Quaternion q = Quaternion.AngleAxis(angle, axis);

            rb.MoveRotation(rb.rotation*q);
            Vector3 newPosition = q*(tragetMovePosition - turnSource.position) + turnSource.position;
            rb.MovePosition(newPosition);

        }
            
    }

    public bool checkIfGrounded()
    {
        Vector3 start = bodyCollider.transform.TransformPoint(bodyCollider.center);
        float rayLength = bodyCollider.height/2 - bodyCollider.radius + 0.05f;

        bool hasHit = Physics.SphereCast(start, bodyCollider.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);

        return hasHit;
    }
}
