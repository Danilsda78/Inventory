using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ButtonSwichPlay _buttonSwichPlay;

    public GamePlay GamePlay;
    public InventoryPlay InvenrotyPlay;

    private void Awake()
    {
        _buttonSwichPlay.EPlayInventory += PlayInventory;
        _buttonSwichPlay.EPlayGame += PlayGame;
    }

    private void Start()
    {
        InvenrotyPlay.Init();
        InvenrotyPlay.Create();
    }

    private void Update()
    {
        GamePlay.Run();
    }

    public void PlayGame()
    {
        GamePlay.Init(InvenrotyPlay.GetInventoryView());
        InvenrotyPlay.Destroy();
    }

    public void PlayInventory()
    {
        InvenrotyPlay.Init();
        InvenrotyPlay.Create();
        GamePlay.Destroy();
    }

    private void OnDestroy()
    {
        InvenrotyPlay.Destroy();
    }
}
