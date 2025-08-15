using System.Collections;
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
    [SerializeField] private PopupView _popupViewPrefab;

    public List<ItemInventoryView> ItemInventoryViews;
    public Inventory Inventory;
    private PopupView _popupView;


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
            newSlot.EOnPointerExit += OnClearSlotsView;
            newSlot.EOnDrop += OnAddItemSlots;
        }

        foreach (var itemView in GetListItemView())
        {
            var itemInventoryView = CreateItemInventiryView(itemView);
            itemInventoryView.EOnBeginDrag += OnRemoveInventoryView;

        }

        StartCoroutine(AsincSetPositionItem());
    }

    public bool SetInventorySize(int x, int y, out Inventory newInventory)
    {
        newInventory = new Inventory(new MyVector2Int(x, y));

        foreach (var keyValue in Inventory.MapItems)
        {
            var isAdd = newInventory.AddItem(keyValue.Value.Slot, keyValue.Value);

            if (isAdd == false)
                return false;
        }

        return true;
    }

    private IEnumerator AsincSetPositionItem()
    {
        yield return null;

        foreach (var itemView in ItemInventoryViews)
            if (_mapSlots.ContainsKey(itemView.ItemView.Item.Slot.Position))
                itemView.transform.position = _mapSlots[itemView.ItemView.Item.Slot.Position].transform.position;
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

    public void OnClearSlotsView()
    {
        foreach (var slot in _mapSlots)
        {
            slot.Value.SetColorDefolte();
        }
    }

    public void OnAddItemSlots(ItemInventoryView ItemInventoryView, SlotUI slotUI)
    {
        var isAdd = Inventory.IsAddItem(slotUI.Slot, ItemInventoryView.ItemView.Item, out var listPosSlots);
        if (isAdd)
        {
            Inventory.AddItem(slotUI.Slot, ItemInventoryView.ItemView.Item);
            ItemInventoryView.EOnBeginDrag += OnRemoveInventoryView;
            ItemInventoryView.transform.position = slotUI.transform.position;
            Debug.Log($"Add '{ItemInventoryView.ItemView.Item}' in invetory to slot '{slotUI.Slot.Position}'");
        }

        OnClearSlotsView();
    }

    public void OnRemoveInventoryView(ItemInventoryView ItemInventoryView, Slot slot)
    {
        Inventory.RemoveItem(slot.Position);

        ItemInventoryView.EOnBeginDrag -= OnRemoveInventoryView;
        Debug.Log($"Remove '{ItemInventoryView.ItemView.Item}' in invetory.");
    }

    public void OnDropInventoryView(ItemInventoryView curentItem, ItemInventoryView toMergItem)
    {
        var res = curentItem.ItemView.Merge(toMergItem.ItemView, out var newItemView);

        if (res)
        {
            var newItemInventory = CreateItemInventiryView(newItemView);
            newItemInventory.transform.position = curentItem.transform.position;

            if (Inventory.GetPositionInMap(curentItem.ItemView.Item, out var pos))
            {
                OnRemoveInventoryView(curentItem, curentItem.ItemView.Item.Slot);
                OnAddItemSlots(newItemInventory, _mapSlots[pos]);
            }

            ItemInventoryViews.Remove(curentItem);
            Destroy(curentItem.gameObject);
            ItemInventoryViews.Remove(toMergItem);
            Destroy(toMergItem.gameObject);
        }
    }

    public ItemView GetNewItemView(Item item)
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
            newList.Add(GetNewItemView(item));

        return newList;
    }

    public ItemInventoryView CreateItemInventiryView(int id)
    {
        ItemView itemView = null;

        foreach (var data in _listItemData)
            foreach (var itemData in data.Lvl)
                if (itemData.Id == id)
                    itemView = ItemView.CreateEmptyItem(id, data);

        var newItemInventiry = Instantiate(_itemInventoryViewPrefab, _transformParantSlots.transform.root);
        newItemInventiry.Init(itemView);
        newItemInventiry.EOnDrop += OnDropInventoryView;
        newItemInventiry.EOnPointer += OpenPopup;
        ItemInventoryViews.Add(newItemInventiry);

        return newItemInventiry;
    }

    public ItemInventoryView CreateItemInventiryView(ItemView itemView)
    {
        var newItemInventiry = Instantiate(_itemInventoryViewPrefab, _transformParantSlots.transform.root);
        newItemInventiry.Init(itemView);
        ItemInventoryViews.Add(newItemInventiry);
        newItemInventiry.EOnDrop += OnDropInventoryView;
        newItemInventiry.EOnPointer += OpenPopup;

        return newItemInventiry;
    }

    public int GetRandomId()
    {
        var rnd = new System.Random();
        var data = _listItemData[rnd.Next(_listItemData.Count)];
        var id = data.Lvl[rnd.Next(data.Lvl.Count)].Id;

        return id;
    }

    private void OpenPopup(ItemView itemView)
    {
        _popupView = Instantiate(_popupViewPrefab, _transformParantSlots.transform.root);
        _popupView.Open(itemView);
    }

    public void OnDestroy()
    {
        foreach (var item in ItemInventoryViews)
            Destroy(item.gameObject);
    }
}


