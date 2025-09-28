using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] protected float lifeTime = 5;

    [Header("Setting Pool")]
    [SerializeField] protected string idForPool = "Bullet";
    [SerializeField] protected GameObject objToDisable;

    [SerializeField] protected float speedBullet = 15;
    [SerializeField] protected int damage = 10;

    public Vector3 direction;

    protected Rigidbody rb;

    public virtual void Awake() => rb = GetComponent<Rigidbody>();

    public virtual void OnEnable()
    {
        StartCoroutine(LifeTimeRoutione());
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
    }

    public void OnShoot(Vector3 direction) => rb.velocity = direction * speedBullet;

    public virtual IEnumerator LifeTimeRoutione()
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnToPool();
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        I_IDamage I_damage = collision.collider.GetComponent<I_IDamage>();

        if (I_damage != null) I_damage.Damage(-damage);
    }

    public virtual void ReturnToPool() { if (ManagerPool.Instance != null) ManagerPool.Instance.ReturnToPool(idForPool, objToDisable); }

    public virtual void OnDisable()
    {
        StopAllCoroutines();

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }
}
