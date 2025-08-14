using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ButtonSwichPlay _buttonSwichPlay;

    public List<ItemData> ListItemData;
    public GamePlay GamePlay;
    public InventoryPlay InvenrotyPlay;

    private void Start()
    {
        _buttonSwichPlay.EPlayGame += PlayGame;
        _buttonSwichPlay.EPlayInventory += PlayInventory;
        PlayerPrefs.DeleteAll();
    }

    public void PlayGame()
    {
        var inv = new Inventory(new MyVector2Int(2,2));
        GamePlay.Init(inv);
        InvenrotyPlay.Destroy();
    }

    public void PlayInventory()
    {
        InvenrotyPlay.Init();
        GamePlay.Destroy();
    }
}
