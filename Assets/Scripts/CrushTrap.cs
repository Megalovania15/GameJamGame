using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CrushTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Start fall coroutine...
    }
}
