using System;
using UnityEngine;

public class ItemTypeRecharg
{
    public Item Item { get; private set; }
    public ItemView ItemView { get; private set; }
    public bool IsReady { get; private set; }
    public ReactProperty<float> CurrentRecharg = new ReactProperty<float>();
    public Action EAction;

    public ItemTypeRecharg(ItemView itemView)
    {
        Item = itemView.Item;
        ItemView = itemView;
    }

    public void Run()
    {
        if (CurrentRecharg.Value > 0)
        {
            CurrentRecharg.Value -= Time.deltaTime;
            IsReady = false;
        }
        else
        {
            CurrentRecharg.Value = 0;
            IsReady = true;
        }
    }

    public void Action()
    {
        CurrentRecharg.Value = ItemView.Data.Recharge;
        IsReady = false;
        EAction?.Invoke();
    }
}
