using UnityEngine;

public class TotemAbility : ActiveAbility
{
    public int NumberOfUses { get; private set; } = 2;

    public override void Activate(Vector2 direction)
    {
        if (NumberOfUses <= 1)
        {
            owner.GetComponent<MaskEquip>().UnequipMask();
        }

        GameObject newTotem = Instantiate(
            prefab, owner.transform.position, Quaternion.identity);
        newTotem.GetComponent<Totem>().Owner = owner;
        NumberOfUses--;
    }
}
