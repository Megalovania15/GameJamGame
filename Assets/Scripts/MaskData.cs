using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Masks/Mask")]
public class MaskData : ScriptableObject
{
    [Header("Visual")]
    public Sprite maskSprite;

    [Header("Abilities")]
    public List<AbilityDefinition> abilties;
}
