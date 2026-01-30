using System;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private GameObject destination;

    private HashSet<GameObject> ignoredObjects = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        try
        {
            if (destination == null)
            {
                Debug.LogWarning("Teleporter (instance ID " + transform.GetInstanceID() + ": destination is null");
                return;
            }
            if (ignoredObjects.Contains(other.gameObject))
            {
                return;
            }
            if (destination.TryGetComponent(out Teleporter teleporter))
            {
                teleporter.ignoredObjects.Add(other.gameObject);
            }
            other.transform.position = destination.transform.position;
        }
        finally
        {
            ignoredObjects.Remove(other.gameObject);
        }
    }
}
