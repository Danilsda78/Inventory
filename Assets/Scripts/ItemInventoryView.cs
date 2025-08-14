using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInventoryView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image _image;
    public Vector2 StartPosition { get; private set; }
    public ItemView ItemView;
    public Action<ItemInventoryView, Slot> EOnBeginDrag;


    public void Init(ItemView itemView)
    {
        ItemView = itemView;
        _image.sprite = ItemView.Sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StartPosition = transform.position;
        _image.raycastTarget = false;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        EOnBeginDrag?.Invoke(this, ItemView.Item.Slot);
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
