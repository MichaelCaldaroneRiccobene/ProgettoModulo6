using UnityEngine;

public class Bullet_Player : Bullet
{
    [Header("Setting Bullet Player")]
    [SerializeField] private float  radius = 15;
    [SerializeField] private float force = 1;

    public override void OnCollisionEnter(Collision collision)
    {
       base.OnCollisionEnter(collision);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            Rigidbody targetRb = collider.GetComponent<Rigidbody>();
            if(targetRb != null)
            {
                Vector3 targetPos = targetRb.position - transform.position;

                targetRb.AddForce(targetPos * force, ForceMode.Impulse);
            }
        }
        gameObject.SetActive(false);
    }
}
