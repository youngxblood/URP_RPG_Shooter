using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBullet : Bullet
{
    protected Rigidbody2D rigidbody2d;

    public override BulletDataSO BulletData
    {
        get => base.BulletData;
        set
        {
            base.BulletData = value;
            rigidbody2d = GetComponent<Rigidbody2D>();
            rigidbody2d.drag = BulletData.Friction;
        }
    }

    private void FixedUpdate()
    {
        rigidbody2d.MovePosition(transform.position + BulletData.BulletSpeed * transform.right * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hittable = collision.GetComponent<IHittable>();
        hittable?.GetHit(BulletData.Damage, gameObject);
        Destroy(gameObject);
    }
}
