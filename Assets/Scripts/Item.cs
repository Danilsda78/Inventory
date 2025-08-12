using System;
using UnityEngine;

[Serializable]
public class Item
{
    public int Id;
    public Slot Slot;
    public float Recharge;
    public Vector2Int[] ListPos;
    
    public Item(ItemDataInfo info)
    {
        Id = info.Lvl[0].Id;
        ListPos = info.ListPos;
        Recharge = info.Recharge;
    }
}