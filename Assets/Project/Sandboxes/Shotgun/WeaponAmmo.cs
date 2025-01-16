using Project.Sandboxes.ScriptableValues;
using UnityEngine;

namespace Project.Sandboxes.Shotgun
{
    public class WeaponAmmo : MonoBehaviour
    {
        [SerializeField] private IntValue currentAmmo;
        [SerializeField] private IntValue maxAmmo;
        
        public bool HasAmmo => currentAmmo.isGreaterThanZero;
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
            currentAmmo.SetValue(maxAmmo.Value);
        }
    
        public void Consume()
        {
            currentAmmo.Value--;
        }

    }
}
