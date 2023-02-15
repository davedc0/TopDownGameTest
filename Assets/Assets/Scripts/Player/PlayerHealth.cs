using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    float armor=0;
    float maxhealthlimit = 5;
    public override void RecieveDammage(float x)
    {
        if (armor > 0)
        {
            armor -= x;
            if (armor < 0)
            {
                armor = 0;
            }
        }
        else if(health>0)
        {
            health -= x;
            if (health <= 0)
            {
                //Destroy(this.gameObject);
                Debug.Log("DEAD");
                GameManager.Instance.EndGame();
            }
        }
        


    }
    public void addArmor(float x)
    {
        armor += x;
        if (armor > 5)
        {
            armor = 5;
        }
    }
    public float getArmor()
    {
        return armor;
    }
    public void IncreaseMaxHealth(float x)
    {
       
        maxHealth += x;
        if (maxHealth > maxhealthlimit)
        {
            maxHealth = maxhealthlimit;
        }
        
        Heal(x);
    }
}
