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
            if (Inventory.GetPositionInMap(curentItem.ItemView.Item, out var pos))
            {
                OnRemoveInventoryView(curentItem, curentItem.ItemView.Item.Slot);
                OnAddItemSlots(CreateItemInventiryView(newItemView), _mapSlots[pos]);
            }
            else
            {
                CreateItemInventiryView(newItemView).transform.position = curentItem.transform.position;
                
                ItemInventoryViews.Remove(curentItem);
                Destroy(curentItem.gameObject);
            }

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
            itemView = ItemView.CreateEmptyItem(id, data);

        var newItemInventiry = Instantiate(_itemInventoryViewPrefab, _transformParantSlots.transform.root);
        newItemInventiry.Init(itemView);
        newItemInventiry.EOnOnDrop += OnDropInventoryView;
        ItemInventoryViews.Add(newItemInventiry);

        return newItemInventiry;
    }

    public ItemInventoryView CreateItemInventiryView(ItemView itemView)
    {
        var newItemInventiry = Instantiate(_itemInventoryViewPrefab, _transformParantSlots.transform.root);
        newItemInventiry.Init(itemView);
        ItemInventoryViews.Add(newItemInventiry);
        newItemInventiry.EOnOnDrop += OnDropInventoryView;

        return newItemInventiry;
    }

    public void OnDestroy()
    {
        foreach (var item in ItemInventoryViews)
            Destroy(item.gameObject);
    }
}


