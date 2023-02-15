using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    float step = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
       
        this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y, this.transform.position.z), step);
    }
}
