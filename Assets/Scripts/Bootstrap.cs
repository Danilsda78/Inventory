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
        //PlayerPrefs.DeleteAll();
        _buttonSwichPlay.EPlayGame += PlayGame;
        _buttonSwichPlay.EPlayInventory += PlayInventory;
        InvenrotyPlay.Init();
        InvenrotyPlay.Create();

    }

    public void PlayGame()
    {
        GamePlay.Init(InvenrotyPlay.GetInventoryView());
        GamePlay.Create();
        InvenrotyPlay.Destroy();
    }

    public void PlayInventory()
    {
        InvenrotyPlay.Create();
        GamePlay.Destroy();
    }
}
