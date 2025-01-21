using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Sandboxes.ScriptableValues
{
    [Serializable]
    public struct OperationResult<T>
    {
        public Operation operationType;
        public T modifierValue;
        public T oldValue;
        public T newValue;
    }

    public abstract class Operation
    {
        public abstract void Execute<T>(T value, T modifierValue, out OperationResult<T> result);
    }

    namespace OperationType
    {
        public class Add : Operation
        {
            public override void Execute<T>(T value, T modifierValue, out OperationResult<T> result)
            {
                result = new OperationResult<T>
                {
                    operationType = this,
                    modifierValue = modifierValue,
                    oldValue = value,
                    newValue = (dynamic)value + (dynamic)modifierValue,
                };
            }
        }

        public class Subtract : Operation
        {
            public override void Execute<T>(T value, T modifierValue, out OperationResult<T> result)
            {
                result = new OperationResult<T>
                {
                    operationType = this,
                    modifierValue = modifierValue,
                    oldValue = value,
                    newValue = (dynamic)value - (dynamic)modifierValue,
                };
            }
        }

        public class Multiply : Operation
        {
            public override void Execute<T>(T value, T modifierValue, out OperationResult<T> result)
            {
                result = new OperationResult<T>
                {
                    operationType = this,
                    modifierValue = modifierValue,
                    oldValue = value,
                    newValue = (dynamic)value * (dynamic)modifierValue,
                };
            }
        }

        public class Set : Operation
        {
            public override void Execute<T>(T value, T modifierValue, out OperationResult<T> result)
            {
                result = new OperationResult<T>
                {
                    operationType = this,
                    modifierValue = modifierValue,
                    oldValue = value,
                    newValue = modifierValue,
                };
            }
        }
    }

    // Shared functionality for all dynamic values, e.g. int, float, etc.
    public class DynamicValue<T> : ScriptableValue<T>
    {
        [HorizontalGroup("ClampValueBool")]
        [SerializeField] private bool useMin;
        [HorizontalGroup("ClampValueBool")]
        [SerializeField] private bool useMax;
        
        [HorizontalGroup("ClampValues")] [ShowIf(nameof(useMin))]
        [SerializeField] private T minValue;
        [HorizontalGroup("ClampValues")] [ShowIf(nameof(useMax))]
        [SerializeField] private T maxValue;

        public bool IsZero => (dynamic)Value == 0;
        public bool IsGreaterThanZero => (dynamic)Value > 0;
        public bool IsLessThanZero => (dynamic)Value < 0;
        
        public T GetMinValue() => minValue;
        public T GetMaxValue() => maxValue;
        
        public new Action<OperationResult<T>> OnValueChanged;
        
        public void SetMinValue(T value)
        {
            minValue = value;
            useMin = true;
        }
        
        public void SetMaxValue(T value)
        {
            maxValue = value;
            useMax = true;
        }

        #region Modify With ScriptableValue
        
        public void Modify<TOperation>(ScriptableValue<T> modifierValue, out OperationResult<T> result) where TOperation : Operation, new()
        {
            new TOperation().Execute(Value, modifierValue.Value, out result);
            OnOperationExecuted(ref result);
            Value = result.newValue;
            OnValueChanged?.Invoke(result);
        }

        public void Modify<TOperation>(ScriptableValue<T> modifierValue) where TOperation : Operation, new()
        {
            Modify<TOperation>(modifierValue, out _);
        }
        
        #endregion

        #region Modify With Normal Value
        
        public void Modify<TOperation>(T modifierValue, out OperationResult<T> result) where TOperation : Operation, new()
        {
            new TOperation().Execute(Value, modifierValue, out result);
            OnOperationExecuted(ref result);
            Value = result.newValue;
            OnValueChanged?.Invoke(result);
        }
        
        public void Modify<TOperation>(T modifierValue) where TOperation : Operation, new()
        {
            Modify<TOperation>(modifierValue, out _);
        }
        
        #endregion
        
        private void OnOperationExecuted(ref OperationResult<T> result)
        {
            if (useMin)
            {
                result.newValue = (dynamic)result.newValue < (dynamic)minValue ? minValue : result.newValue;
            }
            if (useMax)
            {
                result.newValue = (dynamic)result.newValue > (dynamic)maxValue ? maxValue : result.newValue;
            }
        }
    }
}
