using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlay : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private InventoryView _inventoryViewPrefab;
    [SerializeField] private Transform _inventoryViewParent;
    [SerializeField] private List<Transform> _itemPosSpawn;
    [SerializeField] private SetSizeInventory _setSizeInventory;

    private InventoryView _inventoryView;
    private Inventory _inventory;


    public void Init()
    {
        _inventory = GetInventory();
        _setSizeInventory.gameObject.SetActive(true);
        _setSizeInventory.EReaload += Reload;
    }

    public void Create()
    {
        _inventoryView = Instantiate(_inventoryViewPrefab, _inventoryViewParent);
        _inventoryView.Init(_inventory);
        _setSizeInventory.Init(_inventoryView);

        foreach (var item in _itemPosSpawn)
        {
            _inventoryView.CreateItemInventiryView(_inventoryView.GetRandomId()).transform.position = item.position;
        }
    }

    public void Reload(Inventory inventory)
    {
        _inventory = inventory;
        Destroy();
        Init();
        Create();
    }

    public void Destroy()
    {
        if (_inventory != null)
            StoregManager.Save(_inventory);

        if (_inventoryView != null)
            Destroy(_inventoryView.gameObject);

        if (_setSizeInventory != null)
        {
            _setSizeInventory.gameObject.SetActive(false);
            _setSizeInventory.EReaload -= Reload;
            _setSizeInventory.Destroy();
        }
    }

    public Inventory GetInventory()
    {
        Inventory inventory = null;

        if (_inventory == null)
            inventory = StoregManager.Load();
        else
            inventory = _inventory;

        return inventory;
    }
    public InventoryView GetInventoryView()
    {
        if (_inventoryView == null)
            throw new Exception($"InventoryView in {this} is null");

        return _inventoryView;
    }

}
