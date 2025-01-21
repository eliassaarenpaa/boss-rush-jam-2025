using System;
using Project.Sandboxes.ScriptableValues;
using UnityEngine;

namespace Project.Sandboxes.MeatballBossfight
{
    public class DebugHealthText : MonoBehaviour
    {
        [SerializeField] private Damageable damageable;
        [SerializeField] private TMPro.TextMeshPro text;

        private void Start()
        {
            text.text = $"Health: {damageable.Health.Value}";
        }

        private void OnEnable()
        {
            damageable.Health.OnValueChanged += OnHealthChanged;
        }
        
        private void OnDisable()
        {
            damageable.Health.OnValueChanged -= OnHealthChanged;
        }
        
        private void OnHealthChanged(OperationResult<int> result)
        {
            text.text = $"Health: {result.newValue}";
        }
    }
}
