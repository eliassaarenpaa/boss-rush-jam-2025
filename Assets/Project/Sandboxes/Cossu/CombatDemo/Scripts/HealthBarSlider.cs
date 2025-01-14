using UnityEngine;
using UnityEngine.UI;


namespace Project.Sandboxes.Cossu.CombatDemo
{
    [RequireComponent(typeof(Slider))]
    public class HealthBarSlider : MonoBehaviour
    {
        [SerializeField] ScriptableFloat currentHealth;
        [SerializeField] ScriptableFloat maxHealth;
        [SerializeField] Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            slider.maxValue = maxHealth.Value;
            slider.value = currentHealth.Value;
        }

        private void Update()
        {
            slider.value = currentHealth.Value;
            slider.maxValue = maxHealth.Value;
        }
    }
}