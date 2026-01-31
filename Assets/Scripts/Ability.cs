using UnityEngine;

public class Ability : MonoBehaviour
{
    protected GameObject owner;
    protected Transform ownerTransform;
    protected GameObject prefab;
    
    public virtual void OnEquip(GameObject owner, GameObject pref) 
    { 
        this.owner = owner;
        ownerTransform = owner.transform;
       this.prefab = pref;
    }

    public virtual void OnUnequip() { }
}
