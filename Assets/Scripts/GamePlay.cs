using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player playerPref;
    [SerializeField] private Transform playerPos;
    [Header("Enemy")]
    [SerializeField] private Enemy enemyPref;
    [SerializeField] private Transform enemyPos;

    [Header("Item Icon")]
    [SerializeField] private ItemGameplayView _iconItemPrefab;
    [SerializeField] private Transform _iconItemAutoParant;
    [SerializeField] private Transform _iconItemPasivParant;

    private ItemActionManager _itemActionManager;
    private List<ItemGameplayView> _itemView;
    private Player _player;
    private Enemy _enemy;

    public void Init(Inventory inventory)
    {
        _player = Instantiate(playerPref);
        _player.Init(playerPos);

        _enemy = Instantiate(enemyPref);
        _enemy.Init(enemyPos);

        _itemActionManager = new ItemActionManager(inventory, _player, _enemy);
        _itemView = CreateViewItems(_itemActionManager, _iconItemPrefab, _iconItemAutoParant, _iconItemPasivParant);
    }

    public void Run()
    {
        _itemActionManager.Run();

        foreach (var item in _itemView)
        {
            if (_itemActionManager.ListAutoItems.Contains(item.ItemRecharg))
                item.Action();
        }
    }

    public void Destroy()
    {
        _itemActionManager.Dispose();

        Destroy(_player.gameObject);
        Destroy(_enemy.gameObject);

        foreach (var item in _itemView)
            Destroy(item.gameObject);
    }

    private List<ItemGameplayView> CreateViewItems(ItemActionManager ActionManager, ItemGameplayView prefab, Transform transformAutoParant, Transform transformPasivParant)
    {
        var list = new List<ItemGameplayView>();

        foreach (var itemRecharg in ActionManager.ListAutoItems)
        {
            var item = Instantiate(prefab, transformAutoParant);
            item.ItemRecharg = itemRecharg;
            item.Init();
            item.EPresButton += ActionManager.EAction;
            list.Add(item);
        }

        foreach (var itemRecharg in ActionManager.ListPasivItems)
        {
            var item = Instantiate(prefab, transformPasivParant);
            item.ItemRecharg = itemRecharg;
            item.EPresButton += ActionManager.EAction;
            item.Init();
            list.Add(item);
        }

        return list;
    }
}