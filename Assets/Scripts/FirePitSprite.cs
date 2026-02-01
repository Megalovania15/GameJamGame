using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FirePitSprite : MonoBehaviour
{
    private Animator anim;

    public enum FirePitState
    {
        Off = 0,
        Warning = 1,
        Fire = 2,
        Cooldown = 3
    }

    void Awake()
    {
       anim = GetComponent<Animator>();
    }

    public void OnFiredUp()
    {
        SwitchAnimationState(FirePitState.Fire);
    }

    public void OnAboutToFireUp()
    {
        SwitchAnimationState(FirePitState.Warning);
    }

    public void OnCooledDown()
    {
        SwitchAnimationState(FirePitState.Cooldown);
    }

    public void OnTurnedOff()
    {
        SwitchAnimationState(FirePitState.Off);
    }

    public void SwitchAnimationState(FirePitState state)
    {
        switch (state)
        {
            case FirePitState.Off:
                anim.SetInteger("state", 0);
                break;
            case FirePitState.Warning:
                anim.SetInteger("state", 1);
                break;
            case FirePitState.Fire:
                anim.SetInteger("state", 2);
                break;
            case FirePitState.Cooldown:
                anim.SetInteger("state", 3);
                break;
        }
    }
}
