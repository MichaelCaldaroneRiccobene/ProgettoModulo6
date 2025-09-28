using UnityEngine;

public class Bullet_Enemy : Bullet
{
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        ReturnToPool();
    }
}
