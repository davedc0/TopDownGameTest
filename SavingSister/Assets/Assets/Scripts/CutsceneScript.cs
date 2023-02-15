using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneScript : MonoBehaviour
{
    [SerializeField]
    Image img;
    [SerializeField]
    Text textui;
    [SerializeField]
    dialogue[] dial;

    [SerializeField]
    string firstLevel;
    // Start is called before the first frame update
    int index = 0;

    private void Start()
    {
        img.sprite = dial[index].image;
        textui.text = dial[index].text;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            index++;
            if (index >= dial.Length)
            {
                SceneManager.LoadScene(firstLevel);
            }
            else
            {
                img.sprite = dial[index].image;
                textui.text = dial[index].text;
            }
            
        }
       
    }
}

[System.Serializable]
public class dialogue
{
    public Sprite image;
    [TextArea]
    public string text;
}
