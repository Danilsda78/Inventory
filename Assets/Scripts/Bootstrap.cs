using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public List<ItemData> ListItemData;

    public GamePlay gamePlay;

    private void Start()
    {
        gamePlay.Init(new Inventory(new Vector2Int(2,2)));


    }
}
