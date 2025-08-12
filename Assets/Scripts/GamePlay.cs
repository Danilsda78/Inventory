using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public Inventory inventory;
    public ItemData itemDataInfo;
    public Player playerPref;
    public Transform playerPos;
    public Enemy enemyPref;
    public Transform enemyPos;

    public ItemViewGameplay itemView;
    private ItemActionManager itemActionManager;

    void Start()
    {
        inventory = new(new Vector2Int(3, 3));
        var item = new Item(itemDataInfo);
        var slot = new Slot() { Position = new Vector2Int(1, 1) };
        inventory.AddItem(slot, item);

        var player = Instantiate(playerPref);
        player.Init(playerPos);

        var enemy = Instantiate(enemyPref);
        enemy.Init(enemyPos);


        itemActionManager = new ItemActionManager(inventory, player, enemy);
        itemView.Init(itemDataInfo.Lvl[0].Sprite, itemActionManager.Items[0].CurrentRecharg, itemDataInfo.Recharge);
    }

    void Update()
    {
        itemActionManager.Run();

        if (Input.GetMouseButtonDown(0))
            itemActionManager.EAction(ItemType.coffe);


    }
}