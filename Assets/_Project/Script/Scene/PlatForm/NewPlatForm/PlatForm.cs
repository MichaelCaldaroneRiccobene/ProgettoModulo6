using System.Collections.Generic;
using UnityEngine;

public class PlatForm : MonoBehaviour
{
    protected List<Rigidbody> objListOnPlatForm = new List<Rigidbody>();

    protected Rigidbody rb;

    protected float rotationSpeedPlatForm;
    protected float currentVelocity;
    protected float currentRotationSpeedRad;

    protected Quaternion lastRotation;
    protected Vector3 lastPosition;

    protected Vector3 DeltaPlatForm;

    public Rigidbody TemponaryRb {  get; set; }

    public virtual void Start() =>  rb = GetComponent<Rigidbody>();

    public virtual void FixedUpdate()
    {
        Vector3 currentPos = rb.position;

        CalutateVelocity();
        CalculateRotation();

        foreach (Rigidbody rb in objListOnPlatForm) RotationObjInPlatForm(rb, currentPos);

        if(TemponaryRb != null) RotationObjInPlatForm(TemponaryRb, currentPos);

        lastRotation = rb.rotation;
        lastPosition = rb.position;
    }

    private void CalutateVelocity()
    {
        currentVelocity = ((transform.position - lastPosition).magnitude) / Time.fixedDeltaTime;
        DeltaPlatForm = rb.position - lastPosition;
    }

    private void CalculateRotation()
    {
        Quaternion deltaRotation = rb.rotation * Quaternion.Inverse(lastRotation);
        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

        currentRotationSpeedRad = angle / Time.fixedDeltaTime;
        currentRotationSpeedRad *= Mathf.Deg2Rad;
    }

    private void RotationObjInPlatForm(Rigidbody target, Vector3 currentPos )
    {
        float roationAmount = rotationSpeedPlatForm * Time.fixedDeltaTime;

        Quaternion localAngleAxis = Quaternion.AngleAxis(roationAmount,rb.transform.up);
        target.position = (localAngleAxis * (target.position - currentPos)) + currentPos;

        Quaternion globalAngleAxis = Quaternion.AngleAxis(roationAmount,target.transform.InverseTransformDirection(rb.transform.up));
        target.rotation *= globalAngleAxis;

        target.position += (rb.position - lastPosition);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody != null && !other.attachedRigidbody.isKinematic)
        {
            if(!objListOnPlatForm.Contains(other.attachedRigidbody)) objListOnPlatForm.Add(other.attachedRigidbody);
        }
    }

    private void OnExitAddForce(Rigidbody targetRb)
    {
        OnExitAddMoveForce(targetRb);
        OnExitAddRotationForce(targetRb);
    }

    private void OnExitAddMoveForce(Rigidbody targetRb)
    {
        if (DeltaPlatForm.sqrMagnitude > 0.001f)
        {
            Vector3 ForceDirection = DeltaPlatForm.normalized * (currentVelocity / 2);
            targetRb.AddForce(ForceDirection, ForceMode.VelocityChange);
        }
    }

    private void OnExitAddRotationForce(Rigidbody targetRb)
    {
        if (rb == null) rb = GetComponent<Rigidbody>(); // Serve Perche Se no Da problemi O.0

        Vector3 localPosition = targetRb.position - rb.position;
        Vector3 tangentialVelocity = Vector3.Cross(rb.transform.up, localPosition).normalized * currentRotationSpeedRad * localPosition.magnitude;
        targetRb.AddForce(tangentialVelocity, ForceMode.VelocityChange);
    }

    public virtual void OnTriggerExit(Collider other)
    {
        Rigidbody targetRb = other.attachedRigidbody;
        if (targetRb == null || !objListOnPlatForm.Contains(targetRb)) return;

        OnExitAddForce(targetRb);
        objListOnPlatForm.Remove(other.attachedRigidbody);
    }
}
