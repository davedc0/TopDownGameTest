using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootList", menuName = "ScriptableObjects/LootList")]
public class LootList : ScriptableObject
{
    [SerializeField]
    public List<Loots> lootsarray = new List<Loots>();
}
[System.Serializable]
public class Loots
{

    public string itemTag;

    public int percentchance = 0;
}