using System.Collections.Generic;
using UnityEngine;

static public class StoregManager
{
    private const string KEY = "inventory";

    static public void Save(Inventory inventory)
    {
        var data = new InventorySave().SetData(inventory);
        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(KEY, json);
        PlayerPrefs.Save();
    }

    static public Inventory Load()
    {
        if (PlayerPrefs.HasKey(KEY) == false)
            return new Inventory(new MyVector2Int(3, 3));

        string json = PlayerPrefs.GetString(KEY);
        var data = JsonUtility.FromJson<InventorySave>(json);
        var inventory = new InventorySave().GetData(data);

        return inventory;
    }
}

[System.Serializable]
public struct InventorySave
{
    public MyVector2Int Size;

    public SerializableDictionary serializableMapItem;

    public InventorySave SetData(Inventory inventory)
    {
        Size = inventory.Size;
        serializableMapItem = new(inventory.MapItems);
        return this;
    }

    public Inventory GetData(InventorySave data)
    {
        this = data;
        return new Inventory(Size, serializableMapItem.ToDictionary());
    }

}

[System.Serializable]
public class SerializableDictionary
{
    public List<MyVector2Int> keys;
    public List<Item> values;

    public SerializableDictionary(Dictionary<MyVector2Int, Item> dictionary)
    {
        keys = new List<MyVector2Int>(dictionary.Keys);
        values = new List<Item>(dictionary.Values);
    }

    public Dictionary<MyVector2Int, Item> ToDictionary()
    {
        var dictionary = new Dictionary<MyVector2Int, Item>();
        for (int i = 0; i < keys.Count; i++)
        {
            dictionary[keys[i]] = values[i];
        }
        return dictionary;
    }
}