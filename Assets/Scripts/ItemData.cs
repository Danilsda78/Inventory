using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    public string Title;
    public ItemType Type;
    public float Recharge;
    public MyVector2Int[] ListPos;
    public List<ItemDataLvl> Lvl;
    public bool IsAutoAction;

    public int this[int id]
    {
        get
        {
            var i = 0;
            foreach (var lvl in Lvl)
            {
                if (lvl.Id == id)
                    return i;

                i++;
            }

            throw new Exception("Id not found in ItemData");
        }
    }
}


