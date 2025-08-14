using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwichPlay : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _imagePres;
    [SerializeField] private Sprite _imageNotPres;
    [SerializeField] private Button _button;
    private StatePlay _statePlay;
    public Action EPlayGame;
    public Action EPlayInventory;

    private void Awake()
    {
        _button.onClick.AddListener(Swich);
    }

    private void Swich()
    {
        if (_statePlay == StatePlay.PlayInventory)
        {
            _image.sprite = _imagePres;
            _statePlay = StatePlay.PlayGame;
            EPlayGame?.Invoke();
        }
        else
        {
            _image.sprite = _imageNotPres;
            _statePlay = StatePlay.PlayInventory;
            EPlayInventory?.Invoke();
        }
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(Swich);
    }
}

public enum StatePlay
{
    PlayInventory,
    PlayGame,
}
