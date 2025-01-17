using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Sandboxes.OdinSerialization
{
    public class ProjectileComponentHandler : MonoBehaviour
    {
        private List<AbstractBaseType> _components = new List<AbstractBaseType>();
        
        public void AddComponent<T>(T t) where T : AbstractBaseType
        {
            _components.Add(t);
        }
        
        public void RemoveComponent<T>(T t) where T : AbstractBaseType
        {
            _components.Remove(t);
        }

        private void Update()
        {
            foreach (var component in _components)
            {
                component.Update();
            }
        }
        
        private void FixedUpdate()
        {
            foreach (var component in _components)
            {
                component.FixedUpdate();
            }
        }
    }
}
