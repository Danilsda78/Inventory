using System;

[Serializable]
public struct Slot
{
    public MyVector2Int Position {  get; set; }
    public int Id {  get; set; }

    public Slot(MyVector2Int pos, int id)
    {
        Position = pos;
        Id = id;
    }
}
