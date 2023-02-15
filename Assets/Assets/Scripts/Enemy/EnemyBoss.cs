using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject[] BossAdditionalWeapons;

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.TurnBossOn(stat.name);
    }
    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        GameManager.Instance.UpdateBossHP(health.GetHealth(),stat.MaxHealth);
    }
    
    protected override void MoveTo(Vector2 pos)
    {
        base.MoveTo(pos);
        AimWeapons(pos);
    }
    protected override void MoveTo(Vector3 pos)
    {
        base.MoveTo(pos);
        AimWeapons(pos);
    }
    void AimWeapons(Vector2 pos)
    {
        foreach (GameObject weap in BossAdditionalWeapons)
        {
            weap.transform.right = (new Vector2(pos.x, pos.y) - new Vector2(WeaponEquip.transform.position.x, WeaponEquip.transform.position.y)) * -1;
        }
       
    }
    protected override void Update()
    {
        base.Update();
        if (seeplayer && attackTimer < 0 && GameManager.Instance.Player.GetComponent<Player_Movement>().CheckAlive())
        {
            Attack();


        }

    }
    protected override void Attack()
    {
        base.Attack();
        WeaponEquip.GetComponent<EnemyGun>().FireGun();
        foreach (GameObject weap in BossAdditionalWeapons)
        {
            weap.GetComponent<EnemyGun>().FireGun();
        }
    }
    public override void Delete()
    {
        
        base.Delete();
        GameManager.Instance.WinGame();
    }
}
