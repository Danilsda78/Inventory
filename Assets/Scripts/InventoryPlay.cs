using System;
using UnityEngine;

public class InventoryPlay : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private InventoryView _inventoryUIPref;
    [SerializeField] private Transform _inventoryUIParent;

    private InventoryView _inventoryUI;
    private Inventory _inventory;

    private void Start()
    {
        //Init();
    }

    Action<Inventory> callback;
    public void Init()
    {
        var inventory = StoregManager.Load();

        _inventoryUI = Instantiate(_inventoryUIPref, _inventoryUIParent);
        _inventoryUI.Init(inventory);
        _inventory = inventory;
    }

    public void Destroy()
    {
        if (_inventory != null && _inventoryUI != null)
        {
            StoregManager.Save(_inventory);
            Destroy(_inventoryUI.gameObject);
        }
    }
}
