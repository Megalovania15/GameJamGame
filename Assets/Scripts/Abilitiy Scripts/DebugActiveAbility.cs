using UnityEngine;

public class DebugActiveAbility : ActiveAbility
{
    public override void Activate()
    {
        Debug.Log($"{owner.name}: Active ability triggered");
    }
}
