using UnityEngine;

public class InventoryPlay : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private Transform _inventoryUIParent;


    private void Start()
    {
        _inventoryUI = Instantiate(_inventoryUI, _inventoryUIParent);
    }
}
