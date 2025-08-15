[System.Serializable]
public class Item
{
    public int Id;
    public int Lvl;
    public Slot Slot;
    public MyVector2Int[] ListPos;

    public Item() { }

    public Item(Item item)
    {
        Id = item.Id;
        Lvl = item.Lvl;
        Slot = item.Slot;
        ListPos = item.ListPos;
    }
}