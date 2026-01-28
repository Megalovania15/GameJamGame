using UnityEngine;

public class DebugAbility : Ability
{
    public AbilityData data;

    public override void OnEquip(GameObject owner, GameObject pref)
    {
        base.OnEquip(owner, pref);
        Debug.Log($"{name}: DebugAbility equipped on {owner.name}");
    }

    public override void OnUnequip()
    {
        Debug.Log($"{name}: DebugAbility removed");
    }
}
