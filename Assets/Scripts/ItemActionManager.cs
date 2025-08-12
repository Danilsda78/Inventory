using System;
using System.Collections.Generic;

public class ItemActionManager : IDisposable
{
    public Action<ItemType> EAction;
    public List<ItemTypeRecharg> Items = new();

    private Inventory _inventory;
    private Dictionary<ItemType, float[]> _mapItemType = new();
    private Player _player;
    private Enemy _enemy;
    private const int STRONG = 0;
    private const int RECHARG = 1;

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
                _mapItemType[item.Type] = new float[2];
                _mapItemType[item.Type][STRONG] = item.Strong;
                _mapItemType[item.Type][RECHARG] = item.Recharge;
            }
        }

        foreach (var item in _mapItemType)
            Items.Add(new ItemTypeRecharg(item.Key, item.Value[RECHARG]));

        EAction += Action;
    }

    public void Run()
    {
        foreach (var item in Items)
        {
            item.Run();
        }
    }

    public void Action(ItemType itemType)
    {
        float strong;

        if (itemType == ItemType.coffe)
        {
            strong = _mapItemType[ItemType.coffe][STRONG];
            _player.GetHeal(strong);
            GetItemRecharg(itemType)?.Action();
            return;
        }

        if (itemType == ItemType.heal)
        {
            strong = _mapItemType[ItemType.heal][STRONG];
            _player.GetHeal(strong);
            GetItemRecharg(itemType)?.Action();
            return;
        }

        strong = _mapItemType[ItemType.heal][STRONG];
        _enemy.TakeDamage(strong);

        GetItemRecharg(itemType)?.Action();
    }

    private ItemTypeRecharg GetItemRecharg(ItemType itemType)
    {
        foreach (var item in Items)
        {
            if (item.Type == itemType)
                return item;
        }

        return null;
    }

    public void Dispose()
    {
        EAction -= Action;
    }
}