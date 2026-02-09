using System;
using UnityEngine;

public class CultistAbility : ActiveAbility
{
    [SerializeField] private int numberOfUses = 2;

    public override void Activate(Vector2 direction)
    {
        Debug.Log(numberOfUses);
        if (numberOfUses < 0)
        {
            owner.GetComponent<MaskEquip>().UnequipMask();
            return;
        }

        var colliders = Physics2D.OverlapCircleAll(transform.position, 10f);
        foreach (var collider in colliders)
        {
            if (ReferenceEquals(collider.gameObject, owner))
            {
                continue;
            }

            Debug.Log(collider.gameObject + " " + owner.name);
            if (collider.gameObject.GetComponent<IMortal>() is not null)
            {
                var obj = Instantiate(prefab, transform.position, Quaternion.identity);
                var segment = obj.GetComponent<TentacleSegment>();
                segment.Head = true;
                segment.RemainingSegments = 8;
                segment.Target = collider.gameObject;
                segment.Owner = owner;
                break;
            }
        }

        numberOfUses--;

    }

    /*public override void OnUnequip()
    {
        owner.GetComponent<MaskEquip>().UnequipMask();
    }*/
}