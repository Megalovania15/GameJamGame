using UnityEngine;

public abstract class ActiveAbility : Ability
{
    public virtual int NumberOfUses { get; set; } = 3;

    public abstract void Activate(Vector2 direction);

    
}
