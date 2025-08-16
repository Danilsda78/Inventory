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