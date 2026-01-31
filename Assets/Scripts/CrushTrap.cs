using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CrushTrap : MonoBehaviour
{
    [SerializeField] private GameObject girder;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float shakeOffsetY = 0.05f;
    [SerializeField] private float shakePeriod = 0.25f;
    [SerializeField] private float shakeCount = 3;
    [SerializeField] private float dropOffsetY = 1f;
    [SerializeField] private float dropTime = 0.1f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float riseTime = 1f;

    private Vector3 initialPosition;

    private HashSet<GameObject> objectsInDanger = new();
    
    private Coroutine springCoroutine;

    private void Start()
    {
        initialPosition = girder.transform.localPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        springCoroutine ??= StartCoroutine(Crush());
        if (other.gameObject.TryGetComponent(out IMortal mortal))
        {
            Debug.Log("Entered Crush Trap");
            objectsInDanger.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IMortal mortal))
        {
            Debug.Log("Exited Crush Trap");
            objectsInDanger.Remove(other.gameObject);
        }
    }

    private IEnumerator Crush()
    {
        try
        {
            yield return Shake();
            yield return Drop();
            yield return new WaitForSeconds(waitTime);
            yield return Raise();
        }
        finally
        {
            springCoroutine = null;
        }
    }

    private IEnumerator Shake()
    {
        for (var i = 0; i < shakeCount; ++i)
        {
            for (var dt = 0f; dt < shakePeriod; dt += Time.deltaTime)
            {
                var dy = shakeOffsetY * Mathf.Sin((2f * Mathf.PI * dt) / shakePeriod);
                girder.transform.localPosition =
                    new Vector3(initialPosition.x, initialPosition.y + dy, initialPosition.z);
                yield return null;
            }
            yield return null;
        }
    }

    private IEnumerator Drop()
    {
        while (girder.transform.localPosition.y > initialPosition.y - dropOffsetY)
        {
            var localPosition = girder.transform.localPosition;
            localPosition.y -= (1f / dropTime) * Time.deltaTime;
            girder.transform.localPosition = localPosition;
            yield return null;
        }

        foreach (var objectInDanger in objectsInDanger)
        {
            objectInDanger.GetComponent<IMortal>().Die(DeathType.Crushed);
        }
    }

    private IEnumerator Raise()
    {
        while (girder.transform.localPosition.y < initialPosition.y)
        {
            var localPosition = girder.transform.localPosition;
            localPosition.y += (1f / riseTime) * Time.deltaTime;
            girder.transform.localPosition = localPosition;
            yield return null;
        }
    }
}