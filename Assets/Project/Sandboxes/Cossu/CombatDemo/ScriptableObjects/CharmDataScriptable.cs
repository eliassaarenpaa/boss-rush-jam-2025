using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharmObject", menuName = "SO/CharmDataObject")]
public class CharmDataScriptable : ScriptableObject
{
    public List<Modifier> modifiers;
    [SerializeReference]
    public List<ProjectileCustomComponent> customProjectileComponents = new List<ProjectileCustomComponent>();
}