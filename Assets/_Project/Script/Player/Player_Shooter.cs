using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Shooter : MonoBehaviour
{
    [Header("Bullet Setting")]
    [SerializeField] private Bullet bullet;
    [SerializeField] private int damage;
    [SerializeField] protected float speedBullet = 0.5f;

    [Header("Setting Shooting")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform parentBulletPlayer;
    [SerializeField] private float fireRate = 0.5f;

    [Header("Setting Pooling")]
    [SerializeField] private int initialPoolSizeBullet = 20;

    public UnityEvent OnAttack;

    private float lastTimeShoot;
    private bool isOnFocus;
    private List<Bullet> bulletsPool = new List<Bullet>();

    private void Start()
    {
        for (int i = 0; i < initialPoolSizeBullet; ++i) SpawnBullet();
    }

    public void TryToShoot() { if (Time.time - lastTimeShoot >= fireRate && isOnFocus) Shoot(); }

    private void Shoot()
    {
        Bullet b = GetBullet();
   
        b.transform.position = firePoint.position;
        b.Dir = Camera.main.transform.forward;
        b.SpeedBullet = speedBullet;
        b.Damage = damage;

        b.gameObject.SetActive(true);
        OnAttack?.Invoke();

        lastTimeShoot = Time.time;
    }

    public Bullet GetBullet()
    {
        foreach (Bullet b in bulletsPool)
        {
            if (!b.gameObject.activeInHierarchy) return b;
        }
        return SpawnBullet();
    }

    public Bullet SpawnBullet()
    {
        Bullet b;

        if (parentBulletPlayer != null) b = Instantiate(bullet, parentBulletPlayer);
        else b = Instantiate(bullet);
        b.gameObject.SetActive(false);
        bulletsPool.Add(b);
        
        return b;
    }

    public void IsOnFocus(bool isOnFocus) => this.isOnFocus = isOnFocus;
}
