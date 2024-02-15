using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabPhysics : MonoBehaviour
{
    [SerializeField] InputActionProperty grabInputSource;
    [SerializeField] float thresholdValue;
    [SerializeField] float radius = 0.1f;
    [SerializeField] LayerMask grabLayer;

    private FixedJoint fixedJoint;
    private bool isGrabbing = false;



    void FixedUpdate()
    {
        bool isGrabButtonPressed = grabInputSource.action.ReadValue<float>() > thresholdValue;

        if(isGrabButtonPressed && !isGrabbing)
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, radius, grabLayer, QueryTriggerInteraction.Ignore);
        
            if(nearbyColliders.Length > 0)
            {
                Rigidbody nearbyRigidBody = nearbyColliders[0].attachedRigidbody;

                fixedJoint.AddComponent<FixedJoint>();
                fixedJoint.autoConfigureConnectedAnchor = false;

                if(nearbyRigidBody)
                {
                    fixedJoint.connectedBody = nearbyRigidBody;
                    fixedJoint.connectedAnchor = nearbyRigidBody.transform.InverseTransformPoint(transform.position);
                }
                else
                {
                    fixedJoint.connectedAnchor = transform.position;
                }

                isGrabbing = true;
            }
        }
        else if(!isGrabButtonPressed && isGrabbing)
        {
            isGrabbing = false;
            if(fixedJoint)
            {
                Destroy(fixedJoint);
            }
        }
        
    }
}
