using Sirenix.OdinInspector;

namespace Project.Sandboxes.OdinSerialization
{
    [ShowOdinSerializedPropertiesInInspector]
    public abstract class AbstractBaseType
    {
        public int Value { get; set; }

        public abstract void Update();
        public abstract void FixedUpdate();
    }
}
