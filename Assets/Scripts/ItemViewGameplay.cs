using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewGameplay : MonoBehaviour
{
    [SerializeField] private Image _imageItem;
    [SerializeField] private SliderView _sliderView;

    public void Init(Sprite sprite, ReactProperty<float> reactProp, float maxRecharg)
    {
        _imageItem.sprite = sprite;
        _sliderView.Init(maxRecharg, reactProp);
    }
}