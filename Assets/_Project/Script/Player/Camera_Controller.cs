using System;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float distanceToTarget = 5;
    [SerializeField] private float minDistanceToTarget = 1;
    [SerializeField] private float maxDistanceToTarget = 8;

    [SerializeField] private float sensitivityMouse = 3;
    [SerializeField] private float minAngleVertical = -40;
    [SerializeField] private float maxAngleVertical = 80;

    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float collisionRadius = 1;
    [SerializeField] private float smoothLerpToTargetPosition = 10;

    private float yaw;
    private float pitch;
    private float currentDistanceToTarget;

    private Vector3 targetPosition;

    private void Start()
    {
        if (targetPosition == null) return;

        currentDistanceToTarget = distanceToTarget;

        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    private void LateUpdate()
    {
        if (targetPosition == null) return;

        yaw += Input.GetAxis("Mouse X") * sensitivityMouse; pitch -= Input.GetAxis("Mouse Y") * sensitivityMouse;
        pitch = Mathf.Clamp(pitch,minAngleVertical, maxAngleVertical);

        targetPosition = target.position - (Quaternion.Euler(pitch,yaw,0) * Vector3.forward * distanceToTarget);

        //if(Physics.SphereCast(targetPosition,collisionRadius,(targetPosition - transform.position).normalized,out RaycastHit hit, distanceToTarget,collisionMask))
        //{
        //    currentDistanceToTarget = Mathf.Lerp(currentDistanceToTarget,hit.distance,Time.deltaTime * smoothLerpToTargetPosition);
        //}
        //else
        //{
        //    currentDistanceToTarget = Mathf.Lerp(currentDistanceToTarget,distanceToTarget,Time.deltaTime * smoothLerpToTargetPosition);
        //}

        currentDistanceToTarget = Mathf.Lerp(currentDistanceToTarget, distanceToTarget, Time.deltaTime * smoothLerpToTargetPosition);

        transform.position = target.position - (Quaternion.Euler(pitch, yaw, 0) * Vector3.forward * currentDistanceToTarget);
        transform.LookAt(target.position);
    }
}


