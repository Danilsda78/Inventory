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

    public bool Merge(Item item)
    {
        if (item.Id != Item.Id || Data.Lvl.Count < Item.Lvl)
            return false;

        var newLvl = item.Lvl + 1;
        Sprite = Data.Lvl[newLvl].Sprite;
        Strong = Data.Lvl[newLvl].Strong;
        Item.Lvl = newLvl;
        Item.Id = Data.Lvl[newLvl].Id;

        return true;
    }

    static public ItemView CreateItem(int id, ItemData data)
    {
        var index = data[id];
        var newItem = new Item()
        {
            Id = id,
            ListPos = data.ListPos,
            Lvl = index,
        };
        var newItemView = new ItemView(newItem, data);

        return newItemView;
    }
}
