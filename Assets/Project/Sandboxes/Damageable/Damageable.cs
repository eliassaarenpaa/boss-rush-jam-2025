using System;
using Project.Sandboxes.ScriptableValues;
using Project.Sandboxes.ScriptableValues.OperationType;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Sandboxes
{
    public class Damageable : MonoBehaviour
    {
        public Action<int> onTakeDamage;
        
        [SerializeField] private IntValue health;
        
        public IntValue Health => health;
        
        public bool IsAlive => health.IsGreaterThanZero;

        public void InstantiateHealth(IntValue newHealth)
        {
            health = Instantiate(newHealth);
        }
        
        [FoldoutGroup("Debug")][Button]
        private void Take25Damage()
        {
            TakeDamage(25);
        }
        
        [FoldoutGroup("Debug")][Button]
        public void TakeDamage(int damage)
        {
            Debug.Log($"{gameObject.name} took {damage} damage");
            health.Modify<Subtract>(damage);
            onTakeDamage?.Invoke(damage);
        }
        
        [FoldoutGroup("Debug")][Button]
        public void Heal(int amount)
        {
            health.Modify<Add>(amount);
        }
        
        [FoldoutGroup("Debug")][Button]
        public void Kill()
        {
            health.Modify<Set>(0);
        }
    }
}
