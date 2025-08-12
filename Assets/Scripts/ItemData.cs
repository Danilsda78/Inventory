using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    public string Title;
    public ItemType Type;
    public float Recharge;
    public Vector2Int[] ListPos;
    public List<ItemDataLvl> Lvl;
    public bool IsAutoAction;
}


