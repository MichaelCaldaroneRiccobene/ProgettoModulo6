using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] protected float lifeTime = 5;

    public float SpeedBullet { get; set; }
    public Vector3 Dir { get; set; }
    public int Damage { get; set; }

    protected Rigidbody rb;

    public virtual void Awake() => rb = GetComponent<Rigidbody>();

    public virtual void OnEnable()
    {
        rb.isKinematic = false;
        VelocityZero();

        rb.AddForce(Dir.normalized * SpeedBullet, ForceMode.VelocityChange);

        Invoke("Disable", lifeTime);
    }

    public virtual void OnDisable() => CancelInvoke();

    public virtual void Disable()
    {
        VelocityZero();
        rb.isKinematic = true;

        gameObject.SetActive(false);
    }

    private void VelocityZero()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        I_IDamage damage = collision.collider.GetComponent<I_IDamage>();

        if (damage != null) damage.Damage(-Damage);
    }
}
