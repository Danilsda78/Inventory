using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int Id { get; }
    public Slot Slot { get; set; }
    public Vector2Int[] ListPos { get; set; }
    
    public Item(ItemDataInfo info)
    {
        Id = info.Lvl[0].Id;
        ListPos = info.ListPos;
    }
}