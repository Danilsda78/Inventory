using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ButtonSwichPlay _buttonSwichPlay;

    public List<ItemData> ListItemData;
    public GamePlay GamePlay;
    public InventoryPlay InvenrotyPlay;

    private void Awake()
    {
        _buttonSwichPlay.EPlayGame += PlayGame;
        _buttonSwichPlay.EPlayInventory += PlayInventory;
        InvenrotyPlay.Init();
    }

    private void Start()
    {
        InvenrotyPlay.Create();
        
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Backspace))
            PlayerPrefs.DeleteAll();
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
