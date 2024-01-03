using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Slider expSlider;

    public void SetSlider(float amount)
    {
        expSlider.value = amount;
    }
    public void SetSliderMax(float amount)
    {
        expSlider.maxValue = amount;
        SetSlider(amount);
    }
}