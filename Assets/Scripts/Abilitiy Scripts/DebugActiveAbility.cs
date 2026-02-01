using UnityEngine;

public class DebugActiveAbility : ActiveAbility
{
    public override void Activate(Vector2 direction)
    {
        Debug.Log($"{owner.name}: Active ability triggered");
    }
}
