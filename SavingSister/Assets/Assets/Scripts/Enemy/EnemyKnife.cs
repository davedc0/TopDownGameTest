using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnife : Enemy
{
    [SerializeField]
    Animator KnifeAnim;
    
    protected override void Attack()
    {
        base.Attack();
        KnifeAnim.SetTrigger("Attack");
        
    }
    
    protected override void Update()
    {
        base.Update();
        if(Vector2.Distance(GameManager.Instance.Player.transform.position, this.transform.position) < 3&&attackTimer<0&& GameManager.Instance.Player.GetComponent<Player_Movement>().CheckAlive())
        {
            Attack();

        }
       
    }
    
}
