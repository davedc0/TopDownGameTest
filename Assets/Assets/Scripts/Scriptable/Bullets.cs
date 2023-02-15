using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Bullets", menuName = "ScriptableObjects/Bullets")]
public class Bullets : ScriptableObject
{
    public string splatVFX;
    public float lifetime;
    public float Speed;
    public float damage;
}
