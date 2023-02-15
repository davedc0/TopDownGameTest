using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : GunScript
{
    int currentAmmo = 0;
   
    [SerializeField]
    int startammo = 0;
    [SerializeField]
    bool InfiniteFire = false;
    
    // Start is called before the first frame update

    public string GetName()
    {
        return Stat.name;
    }
    public  void SetUpGun()
    {
       
        
            if (startammo > Stat.maxBullets)
            {
                startammo = Stat.maxBullets;
            }
            currentAmmo = startammo;
        
        
    }
    public int CurrentAmmo
    {
        get
        {
            return currentAmmo;
        }
        set
        {
            int v = value;
            if (value > Stat.maxBullets)
            {
                v = Stat.maxBullets;
            }
            else if (value < 0)
            {
                v = 0;
            }
            currentAmmo = v;
        }
    }
    public int StartAmmo
    {
        get
        {
            return startammo;
        }
        set
        {
            int v = value;
            if (value > Stat.maxBullets)
            {
                v = Stat.maxBullets;
            }
            else if (value < 0)
            {
                v = 0;
            }
            startammo = v;
        }
    }
    public void addAmmo(int x)
    {
        currentAmmo += x;
        if (currentAmmo > Stat.maxBullets)
        {
            currentAmmo = Stat.maxBullets;
        }
    }
    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0)&&(currentAmmo>0||InfiniteFire))
        {
            currentAmmo--;

            
            Fire();
        }
        GameManager.Instance.UpdateAmmo(currentAmmo, Stat.maxBullets, InfiniteFire);
    }
}
[System.Serializable]
public class GunData
{
    [SerializeField]
    int ammo;
    [SerializeField]
    GameObject prefabs;

    public GameObject Prefabs
    {
        get
        {
            return prefabs;
        }
    }
    public int Ammo
    {
        get
        {
            return ammo;
        }
        set
        {
            ammo = value;
        }
    }
}
