using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField]
    float damage;
    [SerializeField]
    float range =2;
    [SerializeField]
    GameObject AttackPos;
    public void CheckHit()
    {
        RaycastHit2D hit = Physics2D.BoxCast(AttackPos.transform.position, new Vector2(range, range), 0,this.transform.forward);

        // If it hits something...
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("hit");
                GameManager.Instance.Player.GetComponent<Player_Movement>().Stunned();
                GameManager.Instance.GetDamage(damage);
               
            }
        }
    }
   
}
