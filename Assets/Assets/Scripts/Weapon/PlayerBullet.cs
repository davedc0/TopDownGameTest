using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletScript
{
    // Start is called before the first frame update
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag.Contains("Obstr") || collision.gameObject.tag.Contains("Enemy")||!collision.gameObject.tag.Contains("Bullet"))
        {
            GameObject obj = GameManager.Instance.GetPooledObject(stat.splatVFX);
            if (obj != null)
            {
                obj.SetActive(true);
                obj.transform.position = this.transform.position;
                obj.GetComponent<ParticleSystem>().Play();
            }
           
            KillBullet();

        }
        
        if (collision.gameObject.tag.Contains("Enemy") && !collision.gameObject.tag.Contains("Bullet"))
        {

            collision.gameObject.GetComponent<Enemy>().stunned();
            collision.gameObject.GetComponent<Enemy>().getHealth().RecieveDammage(stat.damage);
        }
    }
    public override void Move()
    {
        base.Move();
        rb.AddForce(transform.right * stat.Speed, ForceMode2D.Impulse);
    }
}
