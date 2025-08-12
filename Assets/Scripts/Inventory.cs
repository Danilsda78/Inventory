using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    public Vector2Int Size { get; private set; }
    public Dictionary<Vector2Int, Slot> MapSlots { get; private set; }
    public Dictionary<Vector2Int, Item> MapItems { get; private set; }

    public Inventory(Vector2Int size, Dictionary<Vector2Int, Item> mapItems = null)
    {
        if (mapItems == null)
            mapItems = new Dictionary<Vector2Int, Item>();

        Size = size;
        MapSlots = new();
        MapItems = mapItems;

        for (int y = 0; y < Size.y; y++)
            for (int x = 0; x < Size.x; x++)
            {
                var posSlot = new Vector2Int(x, y);
                var emptySlot = new Slot(posSlot, -1);
                MapSlots.Add(posSlot, emptySlot);
            }

        foreach (var keyValue in MapItems)
        {
            var item = keyValue.Value;
            var slot = MapSlots[keyValue.Key];
            var res = AddItem(slot, item);

            if (!res)
                throw new Exception("MapItems is greater than MapSlots");
        }
    }

    public bool IsAddItem(Slot slot, Item item, out List<Vector2Int> listPosSlots)
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
                var res = MapSlots[currentPosition].Id == -1 || MapSlots[currentPosition].Id == item.Id;
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

    public bool RemoveItem(Vector2Int itemPos)
    {
        var isNotNull = MapItems.ContainsKey(itemPos);

        if (isNotNull)
        {
            MapItems.Remove(itemPos);

            var emptySlot = new Slot(itemPos, -1);
            MapSlots[itemPos] = emptySlot;
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
}
