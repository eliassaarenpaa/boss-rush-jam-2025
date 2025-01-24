using Project.Sandboxes.ScriptableValues;
using Project.Sandboxes.ScriptableValues.OperationType;
using UnityEngine;

namespace Sandboxes.Cossu.CombatDemo
{
    public class WeaponAmmo : MonoBehaviour
    {
        [SerializeField] private IntValue currentAmmo;
        [SerializeField] private IntValue maxAmmo;

        public bool HasAmmo => currentAmmo.IsGreaterThanZero;
        public bool IsMax => currentAmmo.Value == maxAmmo.Value;

        private void Awake()
        {
            Reload();
        }

        /// <summary>
        ///     Called from animation event
        /// </summary>
        public void Reload()
        {
            currentAmmo.Modify<Set>(maxAmmo.Value);
        }

        public void Consume()
        {
            currentAmmo.Value--;
        }
    }
}
