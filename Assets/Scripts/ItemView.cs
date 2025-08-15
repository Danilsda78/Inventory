using UnityEngine;

public class ItemView
{
    public Item Item;
    public Sprite Sprite;
    public float Strong;
    public ItemData Data { get; private set; }


    public ItemView(Item item, ItemData itemData)
    {
        Item = item;
        Data = itemData;
        Reloud();
    }

    public void Reloud()
    {
        Sprite = Data.Lvl[Item.Lvl].Sprite;
        Strong = Data.Lvl[Item.Lvl].Strong;
        Item.Id = Data.Lvl[Item.Lvl].Id;
    }

    public Sprite GetSprite()
    {
        foreach (var item in Data.Lvl)
            if (Item.Id == item.Id)
                return item.Sprite;

        return null;
    }

    public bool Merge(ItemView item, out ItemView newItemView)
    {
        newItemView = null;

        if (item.Item.Id != Item.Id || Data.Lvl.Count < Item.Lvl)
            return false;

        var newItem = new Item(item.Item);
        newItem.Lvl += 1;
        newItemView = new ItemView(newItem, Data);
        return true;
    }

    static public ItemView CreateEmptyItem(int id, ItemData data)
    {
        var newItem = new Item()
        {
            Id = id,
            ListPos = data.ListPos,
            Lvl = 0,
            Slot = new Slot()
            {
                Id = -1,
                Position = new MyVector2Int(-1, -1)
            }
        };

        var newItemView = new ItemView(newItem, data);

        return newItemView;
    }
}
