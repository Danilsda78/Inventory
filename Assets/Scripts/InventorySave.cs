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

