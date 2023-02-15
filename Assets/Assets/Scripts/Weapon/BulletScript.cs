using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    
    [SerializeField]
    protected Bullets stat;


    protected Rigidbody2D rb;

    
    float KillTImer=0;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public virtual void Move()
    {
        rb = this.GetComponent<Rigidbody2D>();
        
        KillTImer = stat.lifetime;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        KillTImer -= Time.deltaTime;
        if (KillTImer < 0)
        {

            KillBullet();
        }
    }
    protected void KillBullet()
    {
        KillTImer = stat.lifetime;
        gameObject.SetActive(false);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
       
       
    }
   
}
