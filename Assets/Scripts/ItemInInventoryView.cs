using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInInventoryView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image _image;
    public Vector2 StartPosition { get; private set; }
    public Item Item;
    public ItemData Info;
    public Action<ItemInInventoryView, Slot> EOnBeginDrag;

    private void Start()
    {
        Item = new(Info);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StartPosition = transform.position;
        _image.raycastTarget = false;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        EOnBeginDrag?.Invoke(this, Item.Slot);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
    }
}
