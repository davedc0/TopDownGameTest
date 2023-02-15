using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    protected float health;
    [SerializeField]
    protected float maxHealth;
    
    // Start is called before the first frame update
    public virtual void SetHealth(float mhealth)
    {
        maxHealth = mhealth;
        health = maxHealth;
    }
    public virtual void RecieveDammage(float x)
    {
        health -= x;
    }
    public virtual void Heal(float x)
    {
        health += x;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public virtual float GetHealth()
    {
        return health;
    }
}
