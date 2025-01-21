using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharmObject", menuName = "SO/CharmDataObject")]
public class CharmDataScriptable : ScriptableObject
{
    [SerializeReference]
    public List<CustomProjectileComponent> customProjectileComponents = new List<CustomProjectileComponent>();
    public List<Modifier> modifiers;
}