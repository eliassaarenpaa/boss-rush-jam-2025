using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Sandboxes.ScriptableValues
{
    public class ScriptableValue<T> : ScriptableObject
    {
        [ReadOnly]
        [SerializeField] private T _oldValue;
        public T OldValue => _oldValue;

        [OnValueChanged(nameof(HandleValueChanged))]
        [SerializeField] private T _value;
        public T Value
        {
            get => _value;

            set
            {
                if (value.Equals(_value))
                {
                    return;
                }

                _oldValue = _value;
                _value = value;
                HandleValueChanged();
            }
        }
        
        public Action<T> OnValueChanged;

        protected void HandleValueChanged()
        {
            OnValueChanged?.Invoke(_value);
        }
    }
}
