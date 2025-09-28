using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
    [Header("Bullet Setting")]
    [SerializeField] protected string idBullet = "Bullet";
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float speedBullet = 0.5f;

    [Header("Logic Shooting")]
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float fireRate = 1;
    [SerializeField] protected float distanceToShoot = 10;

    public UnityEvent <bool> onRangeTarget;

    protected Transform target;

    protected float lastTimeShoot;
    protected float distanceToTarget;

    protected bool isShooting;

    public virtual void Start()
    {
        if (Game_Manager.Instance != null) target = Game_Manager.Instance.GetTarget();
    }

    public virtual void Update() => TryToShoot();

    public virtual void TryToShoot()
    {
        distanceToTarget = Vector3.Distance(transform.position,target.position);
        if (distanceToTarget >= distanceToShoot)
        {
            onRangeTarget?.Invoke(false);
            return;
        }
        else
        {
            onRangeTarget?.Invoke(true);
            if (!isShooting && Time.time - lastTimeShoot >= fireRate) Shoot();
        }     
    }

    public virtual void Shoot()
    {
        GameObject obj = ManagerPool.Instance.GetGameObjFromPool(idBullet);
        Bullet b = null;

        if(obj.TryGetComponent(out Bullet bullet)) b = bullet;
        if(b == null)
        {
            Debug.LogError("Not Bullet");
            return;
        }

        b.OnShoot(transform.forward);
        b.transform.position = firePoint.position;
        b.gameObject.SetActive(true);

        lastTimeShoot = Time.time;
    }
}
