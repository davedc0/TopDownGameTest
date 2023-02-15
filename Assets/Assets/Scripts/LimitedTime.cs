using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedTime : MonoBehaviour
{
    [SerializeField]
    float TimeLimit = 1;
    float KillTImer = 0;
    // Start is called before the first frame update
    void Start()
    {
        KillTImer = TimeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        KillTImer -= Time.deltaTime;
        if (KillTImer < 0)
        {

            KillTImer = TimeLimit;
            gameObject.SetActive(false);
        }
    }
}
