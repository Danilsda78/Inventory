using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemHeal : MonoBehaviour
{
    [SerializeField] private ItemData _info;
    [SerializeField] private Image _image;

    public int CurrenLvl { get; private set; }
    public int Id { get => _info.Lvl[CurrenLvl].Id; }
    public MyVector2Int[] ListPos { get => _info.ListPos; }
    public Slot CurrentSlot { get; set; }
    private int _maxLvl { get => _info.Lvl.Count; }
    public bool isAuto { get { return _info.IsAutoAction; } }

    private void Start()
    {
        var res = JsonUtility.ToJson(this);
    }

    public void Init(ItemData info, int currenLvl, Slot currentSlot)
    {
        _info = info;
        CurrenLvl = currenLvl;
        CurrentSlot = currentSlot;
    }

    public void Reload()
    {

    }

    public void Action()
    {
        Debug.Log(isAuto);
    }

    //public bool Merge(IItem currItem, out IItem newItem)
    //{
    //    newItem = null;
    //    if (CurrenLvl >= _maxLvl)
    //        return false;

    //    var newLvl = currItem.CurrenLvl + 1;
    //    CurrenLvl = newLvl;
    //    return true;
    //}
}