using System.Collections.Generic;

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