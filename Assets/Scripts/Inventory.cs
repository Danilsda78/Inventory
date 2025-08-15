using System;
using System.Collections.Generic;

[Serializable]
public class Inventory
{
    public MyVector2Int Size { get; private set; }
    public Dictionary<MyVector2Int, Slot> MapSlots { get; set; }
    public Dictionary<MyVector2Int, Item> MapItems { get; set; }

    public Inventory(MyVector2Int size, Dictionary<MyVector2Int, Item> mapItems = null)
    {
        if (mapItems == null)
            mapItems = new();

        Size = size;
        MapSlots = new();
        MapItems = new();

        for (int y = 0; y < Size.y; y++)
            for (int x = 0; x < Size.x; x++)
            {
                var posSlot = new MyVector2Int(x, y);
                var emptySlot = new Slot(posSlot, -1);
                MapSlots.Add(posSlot, emptySlot);
            }

        foreach (var keyValue in mapItems)
        {
            var item = keyValue.Value;
            var slot = MapSlots[keyValue.Key];
            var res = AddItem(slot, item);

            if (!res)
                throw new Exception("MapItems is bigger than MapSlots");
        }
    }

    public bool IsAddItem(Slot slot, Item item, out List<MyVector2Int> listPosSlots)
    {
        var isAdd = true;
        listPosSlots = new();

        foreach (var position in item.ListPos)
        {
            var currentPosition = slot.Position + position;
            var isNotNull = MapSlots.ContainsKey(currentPosition);

            if (isNotNull)
            {
                listPosSlots.Add(currentPosition);
                var res = MapSlots[currentPosition].Id == -1;
                if (!res)
                    isAdd = false;
            }
            else
                isAdd = false;
        }

        return isAdd;
    }

    public bool AddItem(Slot slot, Item item)
    {
        if (IsAddItem(slot, item, out var listPosSlots) == false)
            return false;

        foreach (var position in listPosSlots)
        {
            MapSlots[position] = new Slot(position, item.Id);
        }

        MapItems.Add(slot.Position, item);
        item.Slot = slot;

        return true;
    }

    public bool RemoveItem(MyVector2Int itemPos)
    {
        var isNotNull = MapItems.TryGetValue(itemPos, out var item);

        if (isNotNull)
        {
            foreach (var keyValue in MapSlots)
            {
                if (keyValue.Value.Id == item.Id)
                    keyValue.Value.Id = -1;
            }

            MapItems.Remove(itemPos);

            return true;
        }

        return false;
    }
    
    public List<Item> GetListItems()
    {
        var list = new List<Item>();

        foreach (var item in MapItems)
            list.Add(item.Value);

        return list;
    }

    public bool GetPositionInMap(Item item, out MyVector2Int myVector2Int)
    {
        foreach (var keyValue in MapItems)
        {
            if (keyValue.Value == item)
            {
                myVector2Int = keyValue.Key;
                return true;
            }
        }

        myVector2Int = default;
        return false;
    }
}
