using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class FirePit : MonoBehaviour
{
    [SerializeField] private float onTime = 0.5f;
    [SerializeField] private float offTime = 1f;
    [SerializeField] private float beforeOnWarningTime = 0.3f;
    [SerializeField] private bool startOn;

    [SerializeField] private UnityEvent beforeOnEvent = new();
    [SerializeField] private UnityEvent onEvent = new();
    [SerializeField] private UnityEvent offEvent = new();

    private void Start()
    {
        if (beforeOnWarningTime > offTime)
        {
            Debug.LogWarning("beforeOnWarningTime > offTime. Script may misbehave...");
        }

        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        GetComponent<BoxCollider2D>().enabled = startOn;
        // Too lazy for fixed split based on starting on or off:
        if (startOn)
        {
            onEvent.Invoke();
        }
        else
        {
            offEvent.Invoke();
        }

        // FIXME: this is incorrect for first iteration...
        yield return new WaitForSeconds(startOn ? onTime : offTime);
        for (;;)
        {
            var col = GetComponent<BoxCollider2D>();
            if (col.enabled)
            {
                col.enabled = false;
                offEvent.Invoke();
                yield return new WaitForSeconds(Mathf.Max(offTime - beforeOnWarningTime, 0f));
                beforeOnEvent.Invoke();
                yield return new WaitForSeconds(Mathf.Min(offTime, beforeOnWarningTime));
            }
            else
            {
                col.enabled = true;
                onEvent.Invoke();
                yield return new WaitForSeconds(onTime);
            }
        }
    }
}