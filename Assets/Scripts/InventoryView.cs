using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryView : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _layoutGroup;
    [SerializeField] private Transform _transformParantSlots;
    [SerializeField] private SlotUI _prefSlot;
    [SerializeField] private ItemInventoryView _itemInventoryViewPrefab;
    [SerializeField] private Dictionary<MyVector2Int, SlotUI> _mapSlots;
    [SerializeField] private List<ItemData> _listItemData;

    public List<ItemInventoryView> ItemInventoryViews;
    public Inventory Inventory;


    public void Init(Inventory inventory)
    {
        Inventory = inventory;
        _layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _layoutGroup.constraintCount = Inventory.Size.x;
        _mapSlots = new();

        foreach (var slot in Inventory.MapSlots)
        {
            var newSlot = Instantiate(_prefSlot, _transformParantSlots);
            newSlot.Slot = slot.Value;
            _mapSlots.Add(slot.Key, newSlot);
            newSlot.EOnPointerEnter += OnItemEnterSlots;
            newSlot.EOnPointerExit += OnClearSlotsUI;
            newSlot.EOnDrop += OnAddItemSlots;
        }

        foreach (var itemView in GetListItemView())
        {
            var newItemInventory = CreateItemInventiryView(itemView.Item.Id);
            newItemInventory.transform.position = _mapSlots[itemView.Item.Slot.Position].transform.position;
        }
    }

    private void OnItemEnterSlots(Item item, SlotUI slotUI)
    {
        var isAddItem = Inventory.IsAddItem(slotUI.Slot, item, out var listPosSlots);

        foreach (var pos in listPosSlots)
        {
            var isNotNull = Inventory.MapSlots.ContainsKey(pos);

            if (isNotNull)
                if (isAddItem)
                    _mapSlots[pos].SetColorGreen();
                else _mapSlots[pos].SetColorRed();
        }
    }

    public void OnClearSlotsUI()
    {
        foreach (var slot in _mapSlots)
        {
            slot.Value.SetColorDefolte();
        }
    }

    public void OnAddItemSlots(ItemInventoryView itemUI, SlotUI slotUI)
    {
        var isAdd = Inventory.IsAddItem(slotUI.Slot, itemUI.ItemView.Item, out var listPosSlots);
        if (isAdd)
        {
            Inventory.AddItem(slotUI.Slot, itemUI.ItemView.Item);
            itemUI.EOnBeginDrag += OnRemoveItemUI;
            itemUI.transform.position = slotUI.transform.position;
            Debug.Log($"Add '{itemUI.ItemView.Item}' in invetory to slot '{slotUI.Slot.Position}'");
        }

        OnClearSlotsUI();
    }

    public void OnRemoveItemUI(ItemInventoryView itemUI, Slot slot)
    {
        Inventory.RemoveItem(slot.Position);
        itemUI.EOnBeginDrag -= OnRemoveItemUI;
        Debug.Log($"Remove '{itemUI.ItemView.Item}' in invetory.");
    }

    public ItemView GetItemView(Item item)
    {
        ItemView newItemView = null;

        foreach (var itemData in _listItemData)
            foreach (var itemLvl in itemData.Lvl)
                if (itemLvl.Id == item.Id)
                    newItemView = new ItemView(item, itemData);

        return newItemView;
    }

    public List<ItemView> GetListItemView()
    {
        List<ItemView> newList = new();

        foreach (var item in Inventory.GetListItems())
            newList.Add(GetItemView(item));

        return newList;
    }

    public ItemInventoryView CreateItemInventiryView(int id)
    {
        ItemView itemView = null;

        foreach (var data in _listItemData)
            itemView = ItemView.CreateItem(id, data);

        var newItemInventiry = Instantiate(_itemInventoryViewPrefab, _transformParantSlots.transform.root);
        newItemInventiry.Init(itemView);
        ItemInventoryViews.Add(newItemInventiry);

        return newItemInventiry;
    }

    public void OnDestroy()
    {
        foreach (var item in ItemInventoryViews)
            Destroy(item.gameObject);
    }
}


