using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "DataItem")]
public class ItemDataStatic : ScriptableObject
{
    public string Title;
    public Vector2Int[] ListPos;
    public int Lvl;
    public int Id;
    public Slot Slot {  get; set; }
}


