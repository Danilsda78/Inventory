using UnityEngine;
using UnityEngine.UI;

public class PopupView : MonoBehaviour
{
    [SerializeField] private Text _textTitle;
    [SerializeField] private Text _textStrong;
    [SerializeField] private Text _textLevel;
    [SerializeField] private Button _closeButton;

    public void Open(ItemView item)
    {
        _textTitle.text = item.Data.Title;
        _textStrong.text = item.Strong.ToString();
        _textLevel.text = item.Item.Lvl.ToString();
        _closeButton.onClick.AddListener(Close);
    }

    private void Close()
    {
        Destroy(gameObject);
    }
}
