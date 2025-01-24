using Project.Sandboxes.ScriptableValues;
using Project.Sandboxes.ScriptableValues.OperationType;
using UnityEngine;

namespace Sandboxes.Cossu.CombatDemo
{
    public class WeaponAmmo : MonoBehaviour
    {
        [SerializeField] PlayerInventoryScriptable playerInventory;

        [SerializeField] private IntValue currentAmmo;
        [SerializeField] private IntValue maxAmmo;

        public bool HasAmmo => currentAmmo.IsGreaterThanZero;
        public bool IsMax => currentAmmo.Value == maxAmmo.Value;

        private void Start()
        {
            UpdateAmmoLimit();
        }

        private void OnEnable()
        {
            playerInventory.OnInventoryChanged += UpdateAmmoLimit;
        }

        private void OnDisable()
        {
            playerInventory.OnInventoryChanged -= UpdateAmmoLimit;
        }

        private void UpdateAmmoLimit()
        {
            if(playerInventory.TryGetWeaponData(out WeaponData data))
            {
                currentAmmo.SetMaxValue((int)data.baseStats.MagazineSize.TrueValue);
                maxAmmo.Value = (int)data.baseStats.MagazineSize.TrueValue;
            }
        }

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
