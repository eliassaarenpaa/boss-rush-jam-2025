using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarSlider : MonoBehaviour
{
    [SerializeField] Health trackedHealth;
    [SerializeField] Slider slider;
    
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = trackedHealth.GetMaxHealth();
        slider.value = trackedHealth.GetCurrentHealth();
    }
    
    private void OnEnable()
    {
        trackedHealth.onCurrentHealthChanged += UpdateValue;
        trackedHealth.onMaxHealthChanged += UpdateMaxValue;
    }
    
    private void OnDisable()
    {
        trackedHealth.onCurrentHealthChanged -= UpdateValue;
        trackedHealth.onMaxHealthChanged -= UpdateMaxValue;
    }
    
    private void UpdateValue(float newValue, Health.CurrentHealthChangeType type)
    {
        slider.value = newValue;
    }
    
    private void UpdateMaxValue(float newValue, Health.MaxHealthChangeType type)
    {
        slider.maxValue = newValue;
    }

}
