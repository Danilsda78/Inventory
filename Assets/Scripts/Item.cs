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
    public bool IsActionAuto;
    public ItemData Data {  get; private set; }
    
    public Item(ItemData data)
    {
        Data = data;
        Id = data.Lvl[0].Id;
        Strong = data.Lvl[0].Strong;
        ListPos = data.ListPos;
        Recharge = data.Recharge;
        Type = data.Type;
        IsActionAuto = data.IsAutoAction;
    }

    public Sprite GetSprite()
    {
        foreach (var item in Data.Lvl)
            if (Id == item.Id)
                return item.Sprite;

        return null;
    }
}