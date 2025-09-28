using UnityEngine;

public class Turret_On_Player : Turret
{
    [Header("Logic Look Player")]
    [SerializeField] protected float lookDownForPlayer = -8;

    public override void Update()
    {
        if (target == null) return;

        base.Update();
        transform.LookAt(target.transform.position);
    }

    public override void Shoot()
    {
        GameObject obj = ManagerPool.Instance.GetGameObjFromPool(idBullet);
        Bullet b = null;

        if (obj.TryGetComponent(out Bullet bullet)) b = bullet;
        if (b == null)
        {
            Debug.LogError("Not Bullet");
            return;
        }

        b.OnShoot((target.transform.position - transform.position).normalized);
        b.transform.position = firePoint.position;

        b.gameObject.SetActive(true);
        lastTimeShoot = Time.time;
    }
}
