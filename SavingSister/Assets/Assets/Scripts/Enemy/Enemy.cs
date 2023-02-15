using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected Enemies stat;
    [SerializeField]
    protected GameObject WeaponEquip;
    [SerializeField]
    protected bool Chase = true;
    
    [SerializeField]
    protected Vector2[] movepos;
    [SerializeField]
    float stopTime = 3f;
    [SerializeField]
    protected bool randompatrol = true;
    [SerializeField]
    float R_MoveTime = 3f;
    [SerializeField]
    LootList loot;



    protected EnemyHealth health;
    protected float attackTimer=1;
    protected float stunnedTimer = 0;
    protected float movetimer=0;
    protected float stoptimer = 0;
    protected bool seeplayer = false;

    protected Rigidbody2D rb;
    protected Vector2 movement;
    protected Animator anim;
    protected int nextpatrolmoveindex =0;
    protected Vector2 randmove;
   
    
    public EnemyHealth getHealth()
    {
        return health;
    }
    public virtual void Delete()
    {
        GameObject obj = GameManager.Instance.GetPooledObject("Enemy Basic Explode");
        if (obj != null)
        {
            obj.SetActive(true);
            obj.transform.position = this.transform.position;

        }

        health.SetHealth(stat.MaxHealth);
        if (loot != null)
        {
            List<string> possibleItems= new List<string>();
            int rand = Random.Range(1, 101);
            for(int i = 0; i < loot.lootsarray.Count; i++)
            {
                if (rand <= loot.lootsarray[i].percentchance)
                {
                    possibleItems.Add(loot.lootsarray[i].itemTag);
                }
            }
            if (possibleItems.Count > 0)
            {
                int x = Random.Range(0,possibleItems.Count);
                GameObject lootitem = GameManager.Instance.GetPooledObject(possibleItems[x]);
                if (lootitem != null)
                {
                    lootitem.SetActive(true);
                    lootitem.transform.position = this.transform.position;
                }
               

            }
        }
        GameManager.Instance.AddScore(stat.scoreGet);
        this.gameObject.SetActive(false);

    }
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        movement = Vector2.zero;
        anim = this.GetComponent<Animator>();
        health = this.GetComponent<EnemyHealth>();

        health.SetHealth(stat.MaxHealth);

        movetimer = R_MoveTime;
       
    }
  
    // Update is called once per frame
    protected virtual void Update()
    {
            
            if (stunnedTimer <= 0)
            {
                if (Vector2.Distance(GameManager.Instance.Player.transform.position, this.transform.position) <30)
                {
                    RaycastHit2D[] hit = Physics2D.LinecastAll(this.transform.position, GameManager.Instance.Player.transform.position);

                // If it hits something...
                    foreach(RaycastHit2D h in hit)
                    {
                        if (h.collider != null)
                        {
                            
                            if (h.collider.gameObject.tag == "Player")
                            {
                                seeplayer = true;
                                 randompatrol = true;
                                if (Vector2.Distance(GameManager.Instance.Player.transform.position, this.transform.position) > 2 && Chase)
                                {
                                    MoveTo(GameManager.Instance.Player.transform.position);
                                }
                                else
                                {
                                    movement = Vector2.zero;
                                }
                                 WeaponEquip.transform.right = (GameManager.Instance.Player.transform.position - WeaponEquip.transform.position) * -1;




                        }
                        else if(h.collider.tag.Contains("Obstr"))
                            {
                                movement = Vector2.zero;
                                FlipFace(1);
                                WeaponEquip.transform.right = this.transform.right;
                                Patrol();
                                seeplayer = false;
                                break;
                               
                            }
                        }
                    }
                    
               
                }
                else
                {
               
                Patrol();
                    
                }
                
            }
            else
            {
                stunnedTimer -= Time.deltaTime;
                movement = Vector2.zero;
            }
        
       
        
        
    }
    protected virtual void MoveTo(Vector2 pos)
    {
        movement = (pos - (Vector2)this.transform.position).normalized;
        if (pos.x < this.transform.position.x)
        {
            FlipFace(1);

        }
        else
        {
            FlipFace(-1);

        }
        WeaponEquip.transform.right = (new Vector2(pos.x, pos.y) - new Vector2(WeaponEquip.transform.position.x, WeaponEquip.transform.position.y)) * -1;
    }
    protected virtual void MoveTo(Vector3 pos)
    {
        movement = (pos - this.transform.position).normalized;
        if (pos.x < this.transform.position.x)
        {
            FlipFace(1);

        }
        else
        {
            FlipFace(-1);

        }
        WeaponEquip.transform.right = (new Vector2(pos.x, pos.y) - new Vector2(WeaponEquip.transform.position.x, WeaponEquip.transform.position.y)) * -1;
    }
    protected void Patrol()
    {
        if (!randompatrol)
        {
            movement = Vector2.zero;
           
            if (movepos.Length > 0)
            {
                if (Vector2.Distance(this.transform.position, movepos[nextpatrolmoveindex])> 1 && stoptimer <= 0)
                {
                    MoveTo(movepos[nextpatrolmoveindex]);

                   

                }
                if (Vector2.Distance(this.transform.position, movepos[nextpatrolmoveindex]) <= 1)
                {
                   
                    Debug.Log("stop");
                    stoptimer = stopTime;

                    nextpatrolmoveindex++;
                    if (nextpatrolmoveindex >= movepos.Length)
                    {
                        nextpatrolmoveindex = 0;
                    }
                }

            }
            
        }
        else
        {
            movement = Vector2.zero;

            if (movetimer > 0)
            {
                movement = randmove;

                

            }
        }
    }
    protected virtual void FixedUpdate()
    {
        attackTimer -= Time.deltaTime;
     
        rb.velocity = movement * stat.Speed;

        if (movetimer > 0)
        {
            movetimer -= Time.deltaTime;
            if (movetimer < 0)
            {
                stoptimer = stopTime;
            }
        }
        if (stoptimer > 0|| movetimer < 0)
        {

            stoptimer -= Time.deltaTime;
           
            if (stoptimer <= 0)
            {
                
                
                if (randompatrol)
                {
                    randmove= new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)).normalized;
                    movetimer =R_MoveTime;
                }
               
            }
        }
       


    }
    protected virtual void FlipFace(float x)
    {
        //this.transform.localScale = new Vector3(x * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        if (x == 1)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
            WeaponEquip.GetComponent<SpriteRenderer>().flipY = false;
        }
        else if (x == -1)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
            WeaponEquip.GetComponent<SpriteRenderer>().flipY = true;
        }
       
    }
    protected virtual void Attack()
    {
        attackTimer = stat.timeBetweenAttack;
    }
    public virtual void stunned()
    {
       
        anim.SetTrigger("Stunned");
        stunnedTimer = GameManager.Instance.GetStunTime();
        Invoke("UnStun", GameManager.Instance.GetStunTime()); 
    }
    void UnStun()
    {
        anim.SetTrigger("Recover");
    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.blue;
        if (movepos.Length > 0)
        {
            for (int i = 0; i < movepos.Length; i++)
            {

                Gizmos.DrawWireSphere(movepos[i], 1f);
            }
        }
        
    }
}

