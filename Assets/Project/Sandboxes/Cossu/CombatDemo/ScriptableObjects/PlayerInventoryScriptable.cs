using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeaponInventory", menuName = "SO/WeaponInventoryObject")]
public class PlayerInventoryScriptable : ScriptableObject
{
    private void OnValidate()
    {
        OnInventoryChanged?.Invoke();
    }

    //Actions
    public Action OnTarotEquipped;
    public Action OnCharmAdded;
    public Action OnCharmRemoved;
    public Action OnWeaponDataInitialized;
    public Action OnInventoryChanged;

    [SerializeField] private TarotDataScriptable equippedTarot; //Reference to equipped tarot
    [SerializeField] private List<CharmDataScriptable> equippedCharms; //Reference to equipped charms

    public void AddCharm(CharmDataScriptable charmDataObject)
    {
        equippedCharms.Add(charmDataObject);
        OnCharmAdded?.Invoke();
        OnInventoryChanged?.Invoke();
    }

    public void RemoveCharm(CharmDataScriptable charmDataObject)
    {
        if (equippedCharms.Contains(charmDataObject))
        {
            equippedCharms.Remove(charmDataObject);
            OnInventoryChanged?.Invoke();
            OnCharmRemoved?.Invoke();
        }
    }

    public void SwapTarot(TarotDataScriptable tarotDataObject)
    {
        equippedTarot = tarotDataObject;
        OnInventoryChanged?.Invoke();
        OnTarotEquipped?.Invoke();
    }

    public WeaponData GetWeaponData() //Function that re-initializes WeaponData and applies equipped modifiers
    {
        Debug.Log("Weapon data fetched");
        if (equippedTarot == null)
        {
            Debug.LogError("Tarot not equipped. Cannot get Weapon Data");
            return null;
        }
        TarotDataScriptable copyTarotData = Instantiate(equippedTarot); //make a copy of the equipped tarot
        WeaponData newWeaponData = copyTarotData.weaponData; //Copy the weapon data from the tarot to WeaponData so we can modify and add modifiers etc
        foreach(CharmDataScriptable charm in equippedCharms) //Apply charm data
        {
            newWeaponData.baseStats.AddModifiersToStats(charm.modifiers);
            newWeaponData.customComponents.AddRange(charm.customProjectileComponents);
        }
        OnWeaponDataInitialized?.Invoke();
        return newWeaponData;
    }
}