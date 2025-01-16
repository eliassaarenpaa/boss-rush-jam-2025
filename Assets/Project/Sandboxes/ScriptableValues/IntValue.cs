namespace Project.Sandboxes.ScriptableValues
{
    /// <summary>
    ///     In case we need to add some specific functionality to all int values
    /// </summary>
    [UnityEngine.CreateAssetMenu(fileName = "IntValue", menuName = "Values/IntValue", order = 99999)]
    public class IntValue : ScriptableValue<int>
    {
        public bool isZero => Value == 0;
        public bool isGreaterThanZero => Value > 0;
        public bool isLessThanZero => Value < 0;
    }
}
