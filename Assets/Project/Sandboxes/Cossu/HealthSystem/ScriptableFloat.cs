using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableFloat", menuName = "SO/Variables/Float", order = 5)]
public class ScriptableFloat : ScriptableObject
{
    public float Value => value;

    [SerializeField] private float value;

    public delegate void OnFloatChanged(float newValue, FloatChangeType changeType);
    public OnFloatChanged onFloatChanged;

    public enum FloatChangeType
    {
        Add,
        Decrease,
        Multiply,
        Set
    }

    public void ModifyFloat(float changeValue, FloatChangeType changeType)
    {
        switch (changeType)
        {
            case FloatChangeType.Add:
                value += changeValue;
                onFloatChanged?.Invoke(value, FloatChangeType.Add);
                break;
            case FloatChangeType.Decrease:
                value -= changeValue;
                onFloatChanged?.Invoke(value, FloatChangeType.Decrease);
                break;
            case FloatChangeType.Multiply:
                value *= changeValue;
                onFloatChanged?.Invoke(value, FloatChangeType.Multiply);
                break;
            case FloatChangeType.Set:
                value = changeValue;
                onFloatChanged?.Invoke(value, FloatChangeType.Set);
                break;
        }
    }
}
