using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletScript
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag.Contains("Obstr") || collision.gameObject.tag == "Player")
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
       
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.Player.GetComponent<Player_Movement>().Stunned();
            GameManager.Instance.GetDamage(stat.damage);
        }
    }
    public override void Move()
    {
        base.Move();
        rb.AddForce(-1*transform.right * stat.Speed, ForceMode2D.Impulse);
        
        
    }
}
