using UnityEngine;
using UnityEngine.UI;

public class SliderView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private ReactProperty<float> _reactProp;
    public void Init(float maxValue, ReactProperty<float> reactProp)
    {
        _slider.maxValue = maxValue;
        _slider.value = 0;
        _reactProp = reactProp;
        _reactProp.EChanged += ReloudSlider;
    }

    private void ReloudSlider(float value) => _slider.value = value;


    private void OnDisable()
    {
        if (_reactProp != null)
            _reactProp.EChanged -= ReloudSlider;
    }
}
