using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Enemy
{
    [SerializeField]
    GameObject spawnPos;
    [SerializeField]
    string enemyToSpawn;
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnEnemy = GameManager.Instance.GetPooledObject(enemyToSpawn);
        if (spawnEnemy != null)
        {
            spawnEnemy.SetActive(true);

            spawnEnemy.transform.position = spawnPos.transform.position;
        }
        else
        {
            Debug.Log("enemylimit");
        }
        
        


    }
    protected override void Update()
    {
        base.Update();
        if (Vector2.Distance(GameManager.Instance.Player.transform.position, this.transform.position) < 30 && attackTimer < 0 && GameManager.Instance.Player.GetComponent<Player_Movement>().CheckAlive())
        {
            Attack();

        }

    }
}
