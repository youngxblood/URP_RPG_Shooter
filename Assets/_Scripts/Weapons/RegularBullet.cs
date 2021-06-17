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
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            HitObstacle();
        }

        Destroy(gameObject);
    }

    private void HitObstacle()
    {
        Debug.Log("Hitting obstacle."); //TODO: Need to implement proper collision detection
    }
}
