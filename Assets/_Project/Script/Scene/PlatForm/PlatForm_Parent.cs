using UnityEngine;

public class PlatForm_Parent : MonoBehaviour
{
    [SerializeField] private float forceLivePlatForm = 25;

    private Vector3 velocityPlatForm;
    private Vector3 lastPos;

    private void Start() => lastPos = transform.position;

    private void FixedUpdate()
    {
        velocityPlatForm = transform.position - lastPos;
        lastPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision) => collision.collider.transform.SetParent(transform);

    private void OnCollisionExit(Collision collision)
    {
        collision.collider.transform.SetParent(null);
        if (collision.collider.TryGetComponent(out Rigidbody rb))
        {
            if (rb.isKinematic) return;
            rb.velocity += velocityPlatForm * forceLivePlatForm;
        }
    }
}
