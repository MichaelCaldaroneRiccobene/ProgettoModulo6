//using System.Collections.Generic;
//using UnityEngine;

//public class Control_Turrent : MonoBehaviour
//{
//    [Header("Setting")]
//    [SerializeField] private Bullet bulletPreFab;

//    [Header("Setting Pooling")]
//    [SerializeField] private int initialPoolSizeBullet = 20;

//    public Transform Target {  get; set; }
//    public Transform ParentBulletTurret { get; set; }

//    private List<Bullet> bulletsPool = new List<Bullet>();

//    private void Start()
//    {
//        FindTurret();
//        for (int i = 0; i < initialPoolSizeBullet; i++) SpawnBullet();
//    }

//    private void FindTurret()
//    {
//        for (int i = 0; i < transform.childCount; i++)
//        {
//            Turret turret = transform.GetChild(i).GetComponent<Turret>();
//            if (turret != null)
//            {
//                //if(Target != null) turret.target = Target;
//                //turret.Control_Turrent = this;
//            }
//        }
//    }

//    public Bullet GetBullet()
//    {
//        foreach (Bullet b in bulletsPool)
//        {
//            if (!b.gameObject.activeInHierarchy) return b;
//        }
//        return SpawnBullet();
//    }

//    private Bullet SpawnBullet()
//    {
//        Bullet b;
//        if (ParentBulletTurret != null) b = Instantiate(bulletPreFab, ParentBulletTurret);
//        else b = Instantiate(bulletPreFab);

//        bulletsPool.Add(b);
//        b.gameObject.SetActive(false);

//        return b;
//    }
//}
