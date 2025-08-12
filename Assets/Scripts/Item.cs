using System;
using UnityEngine;

[Serializable]
public class Item
{
    public int Id;
    public ItemType Type;
    public Slot Slot;
    public float Recharge;
    public float Strong;
    public Vector2Int[] ListPos;
    
    public Item(ItemData data)
    {
        Id = data.Lvl[0].Id;
        Strong = data.Lvl[0].Strong;
        ListPos = data.ListPos;
        Recharge = data.Recharge;
        Type = data.Type;
    }
}