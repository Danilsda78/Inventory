using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemGameplayView : MonoBehaviour
{
    [SerializeField] private Image _imageItem;
    [SerializeField] private SliderView _sliderView;
    [SerializeField] private Button Button;
    public ItemTypeRecharg ItemRecharg;
    public Action<ItemType> EPresButton;

    public void Init()
    {
        _sliderView.Init(ItemRecharg.ItemView.Data.Recharge, ItemRecharg.CurrentRecharg);
        _imageItem.sprite = ItemRecharg.ItemView.GetSprite();
        Button.onClick.AddListener(Action);
    }

    public void Action() => EPresButton?.Invoke(ItemRecharg.ItemView.Data.Type);

    private void OnDisable() => Button.onClick.RemoveListener(Action);
}