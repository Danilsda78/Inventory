using System;

[Serializable]
public class Item
{
    public int Id;
    public int Lvl;
    public Slot Slot;
    public MyVector2Int[] ListPos;
}