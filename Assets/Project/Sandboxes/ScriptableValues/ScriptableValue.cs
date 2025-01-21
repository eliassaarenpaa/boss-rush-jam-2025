using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Sandboxes.ScriptableValues
{
    public class ScriptableValue<T> : ScriptableObject
    {
        [OnValueChanged("HandleValueChanged")]
        [SerializeField] private T _value;
        
        public Action<T> OnValueChanged;

        public T Value
        {
            get => _value;

            set
            {
                if (value.Equals(_value))
                {
                    return;
                }

                _value = value;

                HandleValueChanged();
            }
        }
        
        private void HandleValueChanged()
        {
            OnValueChanged?.Invoke(_value);
        }

        public void SetValue(T value)
        {
            Value = value;
        }

        public void SetValue(ScriptableValue<T> value)
        {
            Value = value.Value;
        }


        /// <summary>
        ///     Implicit conversion from ScriptableValue to T
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator T(ScriptableValue<T> value)
        {
            return value.Value;
        }

        /// <summary>
        ///     Implicit conversion from T to ScriptableValue
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator ScriptableValue<T>(T value)
        {
            var instance = CreateInstance<ScriptableValue<T>>();
            instance.Value = value;
            return instance;
        }
    }
}
