using Febucci.UI.Core;
using Project.Sandboxes.ScriptableValues;
using UnityEngine;

namespace Project.Sandboxes.Shotgun
{
    public class WeaponAmmoUI : MonoBehaviour
    {
        [SerializeField] private TypewriterCore ammoTextBackground;
        [SerializeField] private TypewriterCore ammoText;
        
        [SerializeField] private IntValue currentAmmo;
        [SerializeField] private IntValue maxAmmo;

        private void Awake()
        {
            ammoTextBackground.startTypewriterMode = TypewriterCore.StartTypewriterMode.FromScriptOnly;
            ammoText.startTypewriterMode = TypewriterCore.StartTypewriterMode.FromScriptOnly;
        }

        private void OnEnable()
        {
            currentAmmo.OnValueChanged += UpdateUI;
            maxAmmo.OnValueChanged += UpdateUI;
        }

        private void OnDisable()
        {
            currentAmmo.OnValueChanged -= UpdateUI;
            maxAmmo.OnValueChanged -= UpdateUI;
        }

        private void UpdateUI(int _)
        {
            var newAmmoText = currentAmmo.Value + "/" + maxAmmo.Value;
            
            ammoText.StopShowingText();
            ammoText.ShowText(newAmmoText);
            ammoText.StartShowingText(true);
            
            ammoTextBackground.StopShowingText();
            ammoTextBackground.ShowText(newAmmoText);
            ammoTextBackground.StartShowingText(true);
        }
    }
}
