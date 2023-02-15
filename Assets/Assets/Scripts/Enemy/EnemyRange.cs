using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : Enemy
{

    [SerializeField]
    float attackRange=30;
    protected override void Attack()
    {
        base.Attack();
        WeaponEquip.GetComponent<EnemyGun>().FireGun();

    }

    protected override void Update()
    {
        base.Update();
        if (seeplayer&&Vector2.Distance(GameManager.Instance.Player.transform.position, this.transform.position) < attackRange && attackTimer < 0 && GameManager.Instance.Player.GetComponent<Player_Movement>().CheckAlive())
        {
            Attack();


        }

    }
    
}
