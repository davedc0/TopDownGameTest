using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float speed=1;
    [SerializeField]
    GameObject WeaponEquip;
    [SerializeField]
    ParticleSystem smoke;
    [SerializeField]
    float sprintspeedmodifier=2;
    [SerializeField]
    float maxStamina;
    [SerializeField]
    AudioSource walksfx;

    Vector2 movement;
    Rigidbody2D rb;
    Animator anim;
    protected float stunnedTimer = 0;
    protected float staminatimer = 0;
    bool alive=true;
    

    void Start()
    {
        
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        staminatimer = maxStamina;
        walksfx.Play();
    }

    public void setWeapon(GameObject neww)
    {
        WeaponEquip = neww;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            if (stunnedTimer <= 0)
            {
                #region movement
                movement = Vector2.zero;
                int animSpeed = 0;
                if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    movement += new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized;
                    animSpeed++;
                }
                if (Input.GetAxisRaw("Vertical") != 0)
                {
                    movement += new Vector2(0, Input.GetAxisRaw("Vertical")).normalized;
                    animSpeed++;
                }
                movement = movement.normalized * speed;
                if (animSpeed > 0 )
                {
                    if (!smoke.isPlaying)
                    {
                        smoke.Play();
                    }
                    if (!walksfx.isPlaying)
                    {
                        walksfx.UnPause();
                    }
                    
                }
                else if (animSpeed <= 0)
                {
                    walksfx.Pause();
                    smoke.Stop();
                }
                anim.SetFloat("Speed", animSpeed);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if(staminatimer > 0)
                    {
                        movement *= sprintspeedmodifier;
                        staminatimer -= Time.deltaTime;
                    }
                    
                }
                else
                {
                    if (staminatimer < maxStamina)
                    {
                        staminatimer += Time.deltaTime;
                        if (staminatimer > maxStamina)
                        {
                            staminatimer = maxStamina;
                        }
                    }
                    
                }
                #endregion

                Vector2 mousePos = Input.mousePosition;
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
                WeaponEquip.transform.right = worldPosition - new Vector2(WeaponEquip.transform.position.x, WeaponEquip.transform.position.y);
                if (worldPosition.x > transform.position.x)
                {
                    FlipFace(1);
                }
                else
                {
                    FlipFace(-1);
                }

                #region switch Weapon
                if (Input.GetButtonDown("SwitchWeapon"))
                {
                    //Debug.Log("switch");
                    GameManager.Instance.SwitchGun();
                }
                #endregion
            }
            else
            {
                stunnedTimer -= Time.deltaTime;
                movement = Vector2.zero;
            }
        }
        else
        {
            movement = Vector2.zero;
        }

       

    }

    private void FixedUpdate()
    {

        GameManager.Instance.UpdateStamina(staminatimer,maxStamina);
        rb.velocity=movement;
    }
    private void FlipFace(float x)
    {
        this.transform.localScale = new Vector3(x * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        WeaponEquip.transform.localScale = new Vector3(x * Mathf.Abs(WeaponEquip.transform.localScale.x), x * Mathf.Abs(WeaponEquip.transform.localScale.y), WeaponEquip.transform.localScale.z);
    }
    public void Stunned()
    {
        anim.SetTrigger("Stunned");
        stunnedTimer = GameManager.Instance.GetStunTime();
        Invoke("UnStun", GameManager.Instance.GetStunTime());
    }
    public void kill()
    {
        alive = false;
        anim.SetBool("Dead",true);
        WeaponEquip.SetActive(false);
        FlipFace(1);


    }
    public bool CheckAlive()
    {
        return alive;
    }
    void UnStun()
    {
        anim.SetTrigger("Recover");
    }
    
}
