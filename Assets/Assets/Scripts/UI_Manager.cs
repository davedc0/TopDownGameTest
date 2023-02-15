using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    GameObject[] health;
    [SerializeField]
    GameObject[] armor;
    [SerializeField]
    Text AmmoText;
    [SerializeField]
    Animator portraitanim;
    [SerializeField]
    GameObject StaminaBar;
    [SerializeField]
    GameObject BossHP;
    [SerializeField]
    Text BossName;
    [SerializeField]
    Text ScoreText;

    [SerializeField]
    GameObject GameOverPanel;

    [SerializeField]
    GameObject WinPanel;
    [SerializeField]
    Text FinalScoreText;


    // Start is called before the first frame update
    public void UpdateScore(int x)
    {
        ScoreText.text = x.ToString("000000");
        FinalScoreText.text = x.ToString("000000");
    }
    public void BackToMenu()
    {
        GameManager.Instance.GotoLevel("Menu", true);
    }
    public void GotoEndScreen()
    {
        GameManager.Instance.GotoLevel("Ending", true);
    }
    //1 = lose, 2= win
    public void OpenPanel(int type)
    {
        if (type == 1)
        {
            GameOverPanel.SetActive(true);
        }
        else if (type == 2)
        {
            WinPanel.SetActive(true);
        }
    }
    public void TurnOnBoss(string pname)
    {
        BossHP.SetActive(true);
        BossName.gameObject.SetActive(true);
        BossName.text = pname;
    }
    public void updateHealth(float health,float maxhealth)
    {
        BossHP.transform.localScale=new Vector3(health/maxhealth, BossHP.transform.localScale.y, BossHP.transform.localScale.z);
    }
    public void getDamage()
    {
        portraitanim.SetTrigger("Damaged");
    }
    
    public void setAmmoText(int currentammo,int maxammo,bool infinite=false)
    {

        AmmoText.text = currentammo + "/" + maxammo;
        if (infinite)
        {
            AmmoText.text ="Infinite";
        }
    }
    public void UpdateStamina(float x, float y)
    {
        StaminaBar.transform.localScale =new Vector3(x/y,1,1);
    }
    // Update is called once per frame
    public void Updatehealth(float x,float y)
    {
        Debug.Log("health updated");
        for(int i = 0; i < 5; i++)
        {
            if (i < x)
            {
                health[i].SetActive(true);
            }
            else
            {
                health[i].SetActive(false);
            }
            if (i < y)
            {
                armor[i].SetActive(true);
            }
            else
            {
                armor[i].SetActive(false);
            }
        }
    }
}
