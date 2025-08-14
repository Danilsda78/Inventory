using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

static public class StoregManager
{
    private const string KEY = "inventory";

    static public void Save(Inventory inventory)
    {
        var data = new InventorySave().SetData(inventory);

        Debug.Log(data.MapItems);
        string json = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(KEY, json);
        PlayerPrefs.Save();
    }

    static public Inventory Load()
    {
        if (PlayerPrefs.HasKey(KEY) == false)
            return new Inventory(new MyVector2Int(3, 3));

        string json = PlayerPrefs.GetString(KEY);
        Debug.Log(json);

        var data = JsonConvert.DeserializeObject<InventorySave>(json);
        var inventory = new InventorySave().GetData(data);

        Debug.Log(inventory.MapSlots);
        return inventory;
    }
}

public struct InventorySave
{
    public MyVector2Int Size;
    public Dictionary<MyVector2Int, Item> MapItems;

    public InventorySave SetData(Inventory inventory)
    {
        Size = inventory.Size;
        MapItems = inventory.MapItems;
        return this;
    }

    public Inventory GetData(InventorySave data)
    {
        this = data;
        return new Inventory(Size, MapItems);
    }
}