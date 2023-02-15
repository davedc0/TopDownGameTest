using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBox : MonoBehaviour
{
    [SerializeField]
    int type = 0;
    [SerializeField]
    float amount = 1;
    [SerializeField]
    string weaponName = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.HealPlayer(amount,type,weaponName);
           
            GameObject Splat = GameManager.Instance.GetPooledObject("ItemVFX");
            if (Splat != null)
            {
                Splat.SetActive(true);
                Splat.transform.position = this.transform.position;
            }
            
          
           
            this.gameObject.SetActive(false);
        }
    }
}
