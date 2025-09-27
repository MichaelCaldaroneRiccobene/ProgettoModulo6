using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Magic : Turret
{
    [Header("Setting Magic Turret")]
    [SerializeField] private int bulletToShoot;
    [SerializeField] private float timeForNextBulletToShootInSequence;

    public override void Update()
    {
        if (target == null) return;

        base.Update();
        transform.LookAt(target.transform.position);
    }

    public override void Shoot() => StartCoroutine(Shooting());

    private IEnumerator Shooting()
    {
        isShooting = true;
        for (int i = 0; i < bulletToShoot; i++)
        {
            float factor = Random.Range(0, 2) == 0 ? 1 : -1;
            Vector3 randomPos = new Vector3(Random.Range(0.5f, 3) * factor, Random.Range(0.5f, 3) * factor, Random.Range(0.5f, 3) * factor);

            Bullet b = Control_Turrent.GetBullet();          

            b.transform.position = firePoint.position + randomPos;
            b.Dir = target.transform.position - transform.position;
            b.SpeedBullet = speedBullet;
            b.Damage = damage;

            b.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeForNextBulletToShootInSequence);
        }
        isShooting = false;
        lastTimeShoot = Time.time;
    }
}
