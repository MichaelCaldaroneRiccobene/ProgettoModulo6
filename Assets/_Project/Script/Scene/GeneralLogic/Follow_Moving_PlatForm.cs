using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Moving_PlatForm : MonoBehaviour
{
    private Moving_PlatForm moving_PlatForm;
    private Rigidbody rb;
    private Vector3 platformVelocity;

    private void Start() => rb = GetComponent<Rigidbody>();

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Moving_PlatForm platForm)) moving_PlatForm = platForm;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Moving_PlatForm platForm) && platForm == moving_PlatForm)
        {
            if(!rb.isKinematic) rb.velocity += platformVelocity;
            moving_PlatForm = null;
        }
    }

    private void FixedUpdate()
    {
        if(moving_PlatForm != null)
        {
            rb.MovePosition(rb.position + moving_PlatForm.VelocityPlatForm);
            rb.MoveRotation(rb.rotation * moving_PlatForm.RotationPlatForm);
        }       
    }
}
