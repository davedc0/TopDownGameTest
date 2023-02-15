using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
   
   
    [SerializeField]
    protected Guns Stat;
    [SerializeField]
    protected  GameObject ShootPos;
    [SerializeField]
    protected  ParticleSystem sidefireVFX;
    [SerializeField]
    protected  ParticleSystem muzzlefireVFX;

    AudioSource fireAudio;
    // Start is called before the first frame update
    public virtual void Start()
    {
        fireAudio = this.GetComponent<AudioSource>();
        fireAudio.clip = Stat.FireSFX;
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
       
    }
    protected void Fire()
    {
        sidefireVFX.Play();
        muzzlefireVFX.Play();
        fireAudio.Play();
        for(int i = 0; i < Stat.amountperfire; i++)
        {
            GameObject bulletToShoot = GameManager.Instance.GetPooledObject(Stat.bullet);
            if (bulletToShoot != null)
            {
                bulletToShoot.SetActive(true);
                bulletToShoot.transform.position = ShootPos.transform.position;
                float rotationoffset = -1 * Stat.spread / 2.0f + i * Stat.spread / Stat.amountperfire;

                bulletToShoot.transform.rotation = ShootPos.transform.rotation * Quaternion.Euler(0, 0, rotationoffset);
                bulletToShoot.GetComponent<BulletScript>().Move();
                bulletToShoot.GetComponentInChildren<TrailRenderer>().Clear();
            }
            
        }
      
    }

}
