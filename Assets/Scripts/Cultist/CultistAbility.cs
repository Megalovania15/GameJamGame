using System;
using UnityEngine;

public class CultistAbility : ActiveAbility
{
    public override void Activate()
    {
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
                segment.RemainingSegments = 5;
                segment.Target = collider.gameObject;
                segment.Owner = owner;
                break;
            }
        }

        Debug.Log("Used cultist ability!");
    }
}