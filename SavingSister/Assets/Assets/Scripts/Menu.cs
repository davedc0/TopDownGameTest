using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    string firstLevel;
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }
}
