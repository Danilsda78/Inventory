using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color _colorGreen;
    [SerializeField] private Color _colorDefolte;
    [SerializeField] private Color _colorRed;
    [SerializeField] private Image _image;

    public Slot Slot;
    public Action<Item, SlotUI> EOnPointerEnter;
    public Action EOnPointerExit;
    public Action<ItemInInventoryView, SlotUI> EOnDrop;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        var result = eventData.pointerDrag.TryGetComponent<ItemInInventoryView>(out var itemUI);
        if (result == false)
            return;

        EOnDrop?.Invoke(itemUI, this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        var result = eventData.pointerDrag.TryGetComponent<ItemInInventoryView>(out var itemUI);
        if (result == false)
            return;

        EOnPointerEnter?.Invoke(itemUI.Item, this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        var result = eventData.pointerDrag;
        if (result == null)
            return;

        EOnPointerExit?.Invoke();
    }

    public void SetColorGreen()
    {
        _image.color = _colorGreen;
    }

    public void SetColorRed()
    {
        _image.color = _colorRed;
    }

    public void SetColorDefolte()
    {
        _image.color = _colorDefolte;
    }
}
