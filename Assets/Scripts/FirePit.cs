using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider2D))]
public class FirePit : MonoBehaviour
{
    [FormerlySerializedAs("beforeOnWarningTime")] [SerializeField]
    private float firingUpTime = 1f;

    [FormerlySerializedAs("onTime")] [SerializeField]
    private float firedUpTime = 3f;

    [FormerlySerializedAs("offTime")] [SerializeField]
    private float cooledDownTime = 5f;

    [FormerlySerializedAs("startOn")] [SerializeField]
    private bool startFiredUp;

    [FormerlySerializedAs("onEvent")] [SerializeField]
    private UnityEvent onFiredUp = new();

    [FormerlySerializedAs("offEvent")] [SerializeField]
    private UnityEvent onCooledDown = new();

    [FormerlySerializedAs("beforeOnEvent")] [SerializeField]
    private UnityEvent onFiringUp = new();

    private void Start()
    {
        if (startFiredUp)
        {
            StartCoroutine(FiredUpLoop());
        }
        else
        {
            StartCoroutine(CooledDownLoop());
        }
    }

    IEnumerator CooledDownLoop()
    {
        for (;;)
        {
            yield return CoolOff();
            yield return FireUp();
        }
    }

    IEnumerator FiredUpLoop()
    {
        for (;;)
        {
            yield return FireUp();
            yield return CoolOff();
        }
    }

    IEnumerator FireUp()
    {
        onFiringUp.Invoke();
        yield return new WaitForSeconds(firingUpTime);
        GetComponent<BoxCollider2D>().enabled = true;
        onFiredUp.Invoke();
        yield return new WaitForSeconds(firedUpTime);
    }

    IEnumerator CoolOff()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        onCooledDown.Invoke();
        yield return new WaitForSeconds(cooledDownTime);
    }
}