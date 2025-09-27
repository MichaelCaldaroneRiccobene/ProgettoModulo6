using System;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float forceImpulce = 10;
    [SerializeField] private float minForce = 0.5f;
    [SerializeField] private float maxForce = 3;
    [SerializeField] private bool isRandomForce;

    private void Start()
    {
        if (isRandomForce)
        {
            float randomForce = UnityEngine.Random.Range(minForce, maxForce);
            forceImpulce = randomForce;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.collider.attachedRigidbody;
        if (rb == null) return;

        rb.AddForce(Vector3.up * forceImpulce, ForceMode.VelocityChange);
    }
}
