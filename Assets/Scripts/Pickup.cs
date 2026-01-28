using UnityEngine;


[CreateAssetMenu(menuName ="Masks/Pickup")]
public class Pickup : ScriptableObject
{
    [Header("Mask")]
    public MaskData maskData;

    [Header("Visuals")]
    public Sprite maskPickupSprite;
}
