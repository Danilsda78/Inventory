using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemActionManager : IDisposable
{
    public Action<ItemType> EAction;
    public List<ItemTypeRecharg> ListAllItems = new();
    public List<ItemTypeRecharg> ListAutoItems = new();
    public List<ItemTypeRecharg> ListPasivItems = new();

    private InventoryView _inventoryView;
    private Dictionary<ItemType, float[]> _mapItemType = new();
    private Player _player;
    private Enemy _enemy;
    private const int STRONG = 0;

    public ItemActionManager(InventoryView inventoryView, Player player, Enemy enemy)
    {
        _inventoryView = inventoryView;
        _enemy = enemy;
        _player = player;

        foreach (var item in _inventoryView.GetListItemView())
        {
            if (_mapItemType.ContainsKey(item.Data.Type))
                _mapItemType[item.Data.Type][STRONG] += item.Strong;
            else
            {
                _mapItemType.Add(item.Data.Type, new float[2] { item.Strong, item.Data.Recharge });
                var ItemRecharg = new ItemTypeRecharg(item);
                ListAllItems.Add(ItemRecharg);

                if (item.Data.IsAutoAction)
                    ListAutoItems.Add(ItemRecharg);
                else
                    ListPasivItems.Add(ItemRecharg);
            }
        }

        EAction += Action;
    }

    public void Run()
    {
        foreach (var item in ListAllItems)
            item.Run();
    }

    public void Action(ItemType itemType)
    {
        var item = GetItemRecharg(itemType);

        if (item == null || item.IsReady == false)
            return;

        float strong = _mapItemType[itemType][STRONG];

        if (itemType == ItemType.coffe || itemType == ItemType.heal)
        {
            _player?.GetHeal(strong);
            item.Action();
            return;
        }
        else
        {
            _enemy?.TakeDamage(strong);
            item.Action();
            return;
        }
    }

    private ItemTypeRecharg GetItemRecharg(ItemType itemType)
    {
        foreach (var itemTypeRecharg in ListAllItems)
        {
            if (itemTypeRecharg.ItemView.Data.Type == itemType)
                return itemTypeRecharg;
        }

        return null;
    }

    public void Dispose()
    {
        EAction -= Action;
        _enemy = null;
        _player = null;
    }
}