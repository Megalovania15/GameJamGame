using UnityEngine;
using System.Collections.Generic;

public class MaskEquip : MonoBehaviour
{
    [Header("References")]
    [SerializeField] MaskVisual maskVisual;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip equipSound;

    MaskData currentMask;
    readonly List<Ability> equippedAbilities = new();
    readonly List<ActiveAbility> activeAbilities = new();

    public Sprite defaultHead;


    public void EquipMask(MaskData mask)
    {
        UnequipMask();

        currentMask = mask;

        maskVisual.SetMask(mask.maskSprite);

        if (audioSource != null && equipSound != null)
        {
            audioSource.PlayOneShot(equipSound);
        }


        foreach (var abilityDef in mask.abilties)
        {
            var abilityType = abilityDef.abilityScript.GetClass();
            if (!typeof(Ability).IsAssignableFrom(abilityType) ) 
            { continue; }

            var ability = (Ability)gameObject.AddComponent(abilityType);
            ability.OnEquip(gameObject, abilityDef.prefab);
            equippedAbilities.Add(ability);

            if (ability is ActiveAbility active)
            {
                activeAbilities.Add(active);
            }

        }

    }

    public void UnequipMask()
    {
        maskVisual.SetMask(defaultHead);

        foreach (var ability in equippedAbilities)
        {
            ability.OnUnequip();
            Destroy(ability);
        }

        equippedAbilities.Clear();
        activeAbilities.Clear();
        maskVisual.SetMask(null);
        currentMask = null;
        maskVisual.SetMask(defaultHead);

    }

    public void ActivateAbilities()
    {
        foreach (var ability in activeAbilities)
        {
            ability.Activate();
        }
    }


}
