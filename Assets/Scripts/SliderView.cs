using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private Action<float> _onChange;
    public void Init(float maxValue, Action<float> action)
    {
        _slider.maxValue = maxValue;
        action = _onChange;
        _onChange += ReloudSlider;
    }

    private void ReloudSlider(float value) => _slider.value = value;

    private void OnDisable()
    {
        _onChange -= ReloudSlider;
    }
}
