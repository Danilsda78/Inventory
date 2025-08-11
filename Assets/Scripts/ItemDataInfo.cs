using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Info", menuName = "DataItemStatick")]
public class ItemDataInfo : ScriptableObject
{
    public string Title;
    public Vector2Int[] ListPos;
    public List<ItemDataDinamic> Lvl;
    public bool IsAutoAction;
}


