using System;
using UnityEngine;

public class InventoryPlay : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private InventoryView _inventoryViewPrefab;
    [SerializeField] private Transform _inventoryViewParent;

    private InventoryView _inventoryView;
    private Inventory _inventory;


    public void Init()
    {
        _inventory = GetInventory();
    }

    public void Create()
    {
        _inventoryView = Instantiate(_inventoryViewPrefab, _inventoryViewParent);
        _inventoryView.Init(_inventory);
        _inventoryView.CreateItemInventiryView(0).transform.position = transform.position;
    }

    private void Update()
    {

    }

    public void Destroy()
    {
        if (_inventory != null)
            StoregManager.Save(_inventory);

        if (_inventoryView != null)
            Destroy(_inventoryView.gameObject);
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
