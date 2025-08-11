using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    int Id { get; }
    Slot Slot { get; set; }
    List<Vector2Int> ListPos { get; set; }
}