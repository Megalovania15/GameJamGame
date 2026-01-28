using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName ="Masks/Ability Definition")]
public class AbilityDefinition : ScriptableObject
{
    [Tooltip("Ability component to add to the player")]
    public MonoScript abilityScript;

    public GameObject prefab;
}


public class AbilityData
{
    public GameObject prefab;
}
