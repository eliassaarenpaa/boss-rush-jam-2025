using UnityEngine;
using System.Collections.Generic;
using System;
using Sirenix.OdinInspector;

namespace Sandboxes.Cossu.CombatDemo
{
    [CreateAssetMenu(fileName = "TarotObject", menuName = "SO/TarotDataObject")]
    public class TarotDataScriptable : ScriptableObject
    {
        public WeaponData weaponData = new WeaponData();
    }
}