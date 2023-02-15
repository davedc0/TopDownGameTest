using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    float stuntime;
    [SerializeField]
    float startHealth=3;
    [SerializeField]
    GunData[] PlayerGuns;

   
    GameObject[] PlayerGunsInScene;
    int gunindex = 0;

    public static GameManager Instance { get; private set; }
    
    [SerializeField]
    PoolObject[] objectToPool;
    List<GameObject> PooledObject;

   
    GameObject player;
    float limitexpand = 1000;

    GameObject[] equipGun;
    UI_Manager ui;
    PlayerHealth Hp;


    int score = 0;
    int showscore = 0;

   

    
  

    public GameObject Player
    {
        get
        {
            return player;
        }
    }
    public void GotoLevel(string nl,bool del =false)
    {
        saveguns();
        if (del)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(this.gameObject);
        }
        SceneManager.LoadScene(nl);

    }
    public void AddScore(int s)
    {
        Debug.Log("Add Score");
        if (s < 0)
        {
            s = 0;
        }
        score += s;

    }
    public float GetStunTime()
    {
        return stuntime;
    }
   
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                Hp = this.GetComponent<PlayerHealth>();
           
                Hp.SetHealth(startHealth);
                for (int i = 0; i < PlayerGuns.Length; i++)
                {
                    PlayerGuns[i].Ammo = PlayerGuns[i].Prefabs.GetComponent<PlayerGun>().StartAmmo;

                }

            }
       
       
       
       
       
    }
    
    void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1;
        PooledObject = new List<GameObject>();
            for (int i = 0; i < objectToPool.Length; i++)
            {
                for (int j = 0; j < objectToPool[i].amount; j++)
                {
                    GameObject obj = GameObject.Instantiate(objectToPool[i].Items);
                    obj.SetActive(false);
                    PooledObject.Add(obj);
                }

            }


            player = FindObjectOfType<Player_Movement>().gameObject;
            ui = FindObjectOfType<UI_Manager>();
            ui.Updatehealth(Hp.GetHealth(), Hp.getArmor());
            PlayerGunsInScene = new GameObject[PlayerGuns.Length];
            for (int i = 0; i < PlayerGuns.Length; i++)
            {
                Debug.Log("re weapon");
                GameObject obj = GameObject.Instantiate(PlayerGuns[i].Prefabs);
                obj.transform.position = player.transform.position + obj.transform.position;
                obj.transform.SetParent(player.transform);
                obj.SetActive(false);
                PlayerGunsInScene[i] = obj;
                PlayerGunsInScene[i].GetComponent<PlayerGun>().StartAmmo = PlayerGuns[i].Ammo;
                PlayerGunsInScene[i].GetComponent<PlayerGun>().SetUpGun();
                
            }
            PlayerGunsInScene[gunindex].gameObject.SetActive(true);
            player.GetComponent<Player_Movement>().setWeapon(PlayerGunsInScene[gunindex].gameObject);
       
       
    }

    public void saveguns()
    {
        for(int i = 0; i < PlayerGuns.Length; i++)
        {
            PlayerGuns[i].Ammo= PlayerGunsInScene[i].GetComponent<PlayerGun>().CurrentAmmo;
        }
    }
    public void TurnBossOn(string name)
    {
        ui.TurnOnBoss(name);
    }
    public void UpdateBossHP(float hp, float maxhp)
    {
        ui.updateHealth(hp, maxhp);
    }
    public void GetDamage(float x)
    {
        ui.getDamage();
        Hp.RecieveDammage(x);
        ui.Updatehealth(Hp.GetHealth(), Hp.getArmor());
    }
    public void UpdateAmmo(int x,int y,bool z)
    {
        ui.setAmmoText(x,y,z);
    }
    public void UpdateStamina(float x, float y)
    {
        ui.UpdateStamina(x, y);
    }
    public void HealPlayer(float x,int type,string weaponname)
    {
        if (type == 0)
        {
            Hp.Heal(x);
        }
        else if (type == 1)
        {
            Hp.addArmor(x);
        }
        else if (type == 2)
        {
            Hp.IncreaseMaxHealth(x);
        }
        else if (type == 3)
        {
            for(int i = 0; i < PlayerGunsInScene.Length; i++)
            {
                if (PlayerGunsInScene[i].GetComponent<PlayerGun>().GetName()==weaponname)
                {
                    PlayerGunsInScene[i].GetComponent<PlayerGun>().addAmmo((int)x);
                }
            }
        }
        
        ui.Updatehealth(Hp.GetHealth(), Hp.getArmor());
    }
    public void EndGame()
    {
        player.GetComponent<Player_Movement>().kill();
        ui.OpenPanel(1);
        Time.timeScale = 0;
    }
    public void WinGame()
    {
        
        ui.OpenPanel(2);
        ui.UpdateScore(score);
        Time.timeScale = 0;
    }
    public GameObject GetPooledObject(string x)
    {
        for (int i = 0; i < PooledObject.Count; i++)
        {
            if (!PooledObject[i].activeInHierarchy&& PooledObject[i].tag==x)
            {
                return PooledObject[i];
            }

        }
        if (PooledObject.Count < limitexpand)
        {
            for (int i = 0; i < objectToPool.Length; i++)
            {

                if (objectToPool[i].Items.tag == x)
                {
                    if (objectToPool[i].canExpand)
                    {
                        GameObject obj = GameObject.Instantiate(objectToPool[i].Items);
                        obj.SetActive(false);
                        PooledObject.Add(obj);
                        return obj;
                    }
                }

            }
        }
        
        return null;
    }
    public void SwitchGun()
    {
        int temp = gunindex;
        gunindex++;
        if (gunindex >= PlayerGunsInScene.Length)
        {
            gunindex = 0;
        }
        PlayerGunsInScene[gunindex].gameObject.SetActive(true);
       
        player.GetComponent<Player_Movement>().setWeapon(PlayerGunsInScene[gunindex].gameObject);
        PlayerGunsInScene[temp].gameObject.SetActive(false);

    }
    public void switchgun(int x)
    {
        int temp = gunindex;
        if (x >= 0 && x < PlayerGunsInScene.Length)
        {
            gunindex = x;
            PlayerGunsInScene[gunindex].gameObject.SetActive(true);
            player.GetComponent<Player_Movement>().setWeapon(PlayerGunsInScene[gunindex].gameObject);
            PlayerGunsInScene[temp].gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        if (showscore < score)
        {
            showscore += 10;
        }
        else if (showscore > score)
        {
            showscore = score;
        }
        ui.UpdateScore(showscore);
    }
    

}

[System.Serializable]
public class PoolObject{


    public GameObject Items;
    
    public int amount;

    public bool canExpand=false;
}
