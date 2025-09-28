using System.Collections;
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


            GameObject obj = ManagerPool.Instance.GetGameObjFromPool(idBullet);
            Bullet b = null;

            if (obj.TryGetComponent(out Bullet bullet)) b = bullet;
            if (b == null)
            {
                Debug.LogError("Not Bullet");
                yield break;
            }

            b.OnShoot((target.transform.position - transform.position).normalized);
            b.transform.position = firePoint.position + randomPos;

            b.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeForNextBulletToShootInSequence);
        }
        isShooting = false;
        lastTimeShoot = Time.time;
    }
}
