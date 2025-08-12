using System;
using UnityEngine;

public class ItemTypeRecharg
{
    public ItemType Type { get; private set; }
    public ReactProperty<float> CurrentRecharg;
    public Action EReady;
    private float _recharg;

    public ItemTypeRecharg(ItemType itemType, float recharg)
    {
        CurrentRecharg = new ReactProperty<float>(0);
        Type = itemType;
        _recharg = recharg;
    }

    public void Run()
    {
        if (CurrentRecharg.Value > 0)
        {
            CurrentRecharg.Value -= Time.deltaTime;
        }
        else
        {
            CurrentRecharg.Value = 0;
            EReady?.Invoke();
        }
    }

    public void Action()
    {
        CurrentRecharg.Value = _recharg;
    }
}
