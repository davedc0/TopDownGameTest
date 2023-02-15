using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Guns", menuName = "ScriptableObjects/Guns")]
public class Guns : ScriptableObject
{
    public string name;
    public string bullet;
    public int maxBullets;
    public float amountperfire=1;
    public float spread;
    public AudioClip FireSFX;

}
