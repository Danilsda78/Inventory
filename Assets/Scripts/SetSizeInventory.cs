using System;
using UnityEngine;
using UnityEngine.UI;

public class SetSizeInventory : MonoBehaviour
{
    [SerializeField] private Slider sliderX;
    [SerializeField] private Slider sliderY;
    public Action<Inventory> EReaload;

    private InventoryView _inventoryView;

    public void Init(InventoryView inventoryView)
    {
        _inventoryView = inventoryView;
        sliderX.value = _inventoryView.Inventory.Size.x;
        sliderY.value = _inventoryView.Inventory.Size.y;

        sliderX.onValueChanged.AddListener(SetX);
        sliderY.onValueChanged.AddListener(SetY);
    }

    public void SetX(float x)
    {
        var oldInt = sliderX.value;
        var res = _inventoryView.SetInventorySize((int)x, (int)sliderY.value, out var newInventory);

        if (res)
            EReaload?.Invoke(newInventory);
        else
            sliderX.value = oldInt;
    }

    public void SetY(float y)
    {
        var oldInt = sliderY.value;
        var res = _inventoryView.SetInventorySize((int)sliderX.value, (int)y, out var newInventory);

        if (res)
            EReaload?.Invoke(newInventory);
        else
            sliderY.value = oldInt;
    }

    public void Destroy()
    {
        sliderX.onValueChanged.RemoveListener(SetX);
        sliderY.onValueChanged.RemoveListener(SetY);
    }
}
