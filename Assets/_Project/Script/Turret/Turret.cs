using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
    [Header("Bullet Setting")]
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float speedBullet = 0.5f;

    [Header("Logic Shooting")]
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float fireRate = 1;
    [SerializeField] protected float distanceToShoot = 10;

    public UnityEvent <bool> onRangeTarget;
    public Control_Turrent Control_Turrent { get; set; }

    protected Transform target;

    protected float lastTimeShoot;
    protected float distanceToTarget;

    protected bool isShooting;

    public virtual void Start()
    {
        Control_Turrent = FindAnyObjectByType<Control_Turrent>();

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
        Bullet b = Control_Turrent.GetBullet();
        
        b.transform.position = firePoint.position;
        b.Damage = damage;
        b.Dir = transform.forward;
        b.SpeedBullet = speedBullet;

        b.gameObject.SetActive(true);
        lastTimeShoot = Time.time;
    }
}
