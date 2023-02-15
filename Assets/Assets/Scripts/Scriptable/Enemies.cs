using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "ScriptableObjects/Enemies")]
public class Enemies : ScriptableObject
{
    public string name;
    public float MaxHealth;
    public float Speed;
    public float timeBetweenAttack;
    public int scoreGet = 100;
    
}
