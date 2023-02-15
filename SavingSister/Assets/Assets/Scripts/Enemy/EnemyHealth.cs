using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{

    public override void RecieveDammage(float x)
    {
        base.RecieveDammage(x);
        if (health <= 0)
        {
            //Destroy(this.gameObject);
            this.GetComponent<Enemy>().Delete();
        }
       
        
    }

    // Start is called before the first frame update

}
