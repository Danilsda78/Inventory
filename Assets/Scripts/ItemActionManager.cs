using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemActionManager : IDisposable
{
    public Action<ItemType> EAction;
    public List<ItemTypeRecharg> ListAllItems = new();
    public List<ItemTypeRecharg> ListAutoItems = new();
    public List<ItemTypeRecharg> ListPasivItems = new();

    private Inventory _inventory;
    private Dictionary<ItemType, float[]> _mapItemType = new();
    private Player _player;
    private Enemy _enemy;
    private const int STRONG = 0;

    public ItemActionManager(Inventory inventory, Player player, Enemy enemy)
    {
        _inventory = inventory;
        _enemy = enemy;
        _player = player;

        foreach (var item in _inventory.GetListItems())
        {
            if (_mapItemType.ContainsKey(item.Type))
                _mapItemType[item.Type][STRONG] += item.Strong;
            else
            {
                _mapItemType.Add(item.Type, new float[2] { item.Strong, item.Recharge });
                var ItemRecharg = new ItemTypeRecharg(item);
                ListAllItems.Add(ItemRecharg);

                if (item.IsActionAuto)
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
            _player.GetHeal(strong);
            item.Action();
            return;
        }
        else
        {
            _enemy.TakeDamage(strong);
            item.Action();
            return;
        }
    }

    private ItemTypeRecharg GetItemRecharg(ItemType itemType)
    {
        foreach (var item in ListAllItems)
        {
            if (item.Item.Type == itemType)
                return item;
        }

        return null;
    }

    public void Dispose()
    {
        EAction -= Action;
    }
}