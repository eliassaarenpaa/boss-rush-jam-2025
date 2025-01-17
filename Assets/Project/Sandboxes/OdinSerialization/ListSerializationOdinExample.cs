using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Sandboxes.OdinSerialization
{
    [CreateAssetMenu(fileName = "ListSerializationOdinExample", menuName = "OdinSerialization/ListSerializationOdinExample", order = 99999)]
    public class ListSerializationOdinExample : SerializedScriptableObject
    {
        [SerializeField] private List<AbstractBaseType> _components = new List<AbstractBaseType>();
    }
}
