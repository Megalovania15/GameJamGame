using UnityEngine;

public class TotemAbility : ActiveAbility
{
    public override int NumberOfUses { get; set; } = 2;

    public override void Activate()
    {
        if (NumberOfUses > 0)
        {
            GameObject newTotem = Instantiate(
                prefab, owner.transform.position, Quaternion.identity);
            newTotem.GetComponent<Totem>().Owner = owner;
            NumberOfUses--;
        }

        //OnUnequip();
    }

    public override void OnUnequip()
    {
        owner = null;
    }
}
