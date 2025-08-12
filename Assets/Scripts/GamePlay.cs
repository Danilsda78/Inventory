using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public Inventory inventory;
    public ItemDataInfo itemDataInfo;
    public Player playerPref;
    public Transform playerPos;
    public Enemy enemyPref;
    public Transform enemyPos;
    public Sprite Sprite;

    void Start()
    {
        inventory = new(new Vector2Int(3, 3));
        var item = new Item(itemDataInfo);
        var slot = new Slot() { Position = new Vector2Int(1, 1) };
        inventory.AddItem(slot, item);

        var player = Instantiate(playerPref);
        player.Init(playerPos);

        var enemy = Instantiate(enemyPref);
        enemy.Init(enemyPos);

    }

    void Update()
    {

    }
}

public class ItemActionManager
{
    private Inventory _inventory;

    private List<ItemRecharg> _items = new();
    private Player _player;
    private Enemy _enemy;

    public ItemActionManager(Inventory inventory)
    {
        _inventory = inventory;
        foreach (var item in _inventory.GetListItems())
            _items.Add(new ItemRecharg(item));

    }

    public void Run()
    {
        foreach (var item in _items)
        {
            item.Run();
        }
    }

    public void ActionHeal()
    {
        _player.GetHeal(100);
    }
}

public class ItemRecharg
{
    public Item Item { get; private set; }
    public ReactProperty<float> CurrentRecharg { get; private set; }
    public bool IsReady { get; private set; }

    public ItemRecharg(Item item)
    {
        CurrentRecharg = new ReactProperty<float>(0);
        Item = item;
    }

    public void Run()
    {
        if (CurrentRecharg.Value > 0)
        {
            CurrentRecharg.Value -= Time.deltaTime;
            IsReady = false;
        }
        else
            IsReady = true;
    }

    public void Action()
    {
        CurrentRecharg.Value = Item.Recharge;
        IsReady = false;
    }
}
