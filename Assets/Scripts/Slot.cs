using UnityEngine;

public struct Slot
{
    public Vector2Int Position;
    public int Id;

    public Slot(Vector2Int pos, int id)
    {
        Position = pos;
        Id = id;
    }
}
