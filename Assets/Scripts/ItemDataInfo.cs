using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Info", menuName = "DataItemInfo")]
public class ItemDataInfo : ScriptableObject
{
    public string Title;
    public float Recharge;
    public Vector2Int[] ListPos;
    public List<ItemDataDinamic> Lvl;
    public bool IsAutoAction;
}


