using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _layoutGroup;
    [SerializeField] private Vector2Int _sizeInventory;
    [SerializeField] private Transform _transformParantSlots;
    [SerializeField] private SlotUI _prefSlot;
    [SerializeField] private List<ItemUI> _listPrefItems;
    [SerializeField] private Dictionary<Vector2Int, SlotUI> _mapSlots;

    private Inventory _inventory;


    private void Start()
    {
        Init(_sizeInventory, new());

    }

    public void Init(Vector2Int sizeInventory, Dictionary<Vector2Int, IItem> mapItems)
    {
        _inventory = new Inventory(sizeInventory, mapItems);
        _layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _layoutGroup.constraintCount = _inventory.Size.x;
        _mapSlots = new();

        foreach (var slot in _inventory.MapSlots)
        {
            var newSlot = Instantiate(_prefSlot, _transformParantSlots);
            newSlot.Slot = slot.Value;
            _mapSlots.Add(slot.Key, newSlot);
            newSlot.EOnPointerEnter += OnItemEnterSlots;
            newSlot.EOnPointerExit += OnClearSlotsUI;
            newSlot.EOnDrop += OnAddItemSlots;
        }

        foreach (var keyValue in mapItems)
        {
            var item = keyValue.Value;
            var itemPos = keyValue.Key;
            var slotUI = _mapSlots[itemPos];

            foreach (var itemUI in _listPrefItems)
            {
                if (itemUI.Item.Id == item.Id)
                {
                    Instantiate(itemUI).transform.position = slotUI.transform.position;
                    break;
                }
            }
        }
    }

    private void OnItemEnterSlots(IItem item, SlotUI slotUI)
    {
        var isAddItem = _inventory.IsAddItem(slotUI.Slot, item, out var listPosSlots);

        foreach (var pos in listPosSlots)
        {
            var isNotNull = _inventory.MapSlots.ContainsKey(pos);

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

    public void OnAddItemSlots(ItemUI itemUI, SlotUI slotUI)
    {
        var isAdd = _inventory.IsAddItem(slotUI.Slot, itemUI.Item, out var listPosSlots);
        if (isAdd)
        {
            _inventory.AddItem(slotUI.Slot, itemUI.Item);
            itemUI.EOnBeginDrag += OnRemoveItemUI;
            itemUI.transform.position = slotUI.transform.position;
            Debug.Log($"Add '{itemUI.Item}' in invetory to slot '{slotUI.Slot.Position}'");
        }

        OnClearSlotsUI();
    }

    public void OnRemoveItemUI(ItemUI itemUI, Slot slot)
    {
        _inventory.RemoveItem(slot.Position);
        itemUI.EOnBeginDrag -= OnRemoveItemUI;
        Debug.Log($"Remove '{itemUI.Item}' in invetory.");
    }
}


