using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TentacleSegment : MonoBehaviour
{
    [SerializeField] private float segmentCreationTime = 1f;
    [SerializeField] private float tentacleRange = 2f;

    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private GameObject tentaclePrefab;

    [SerializeField] private GameObject target;

    [SerializeField] private bool head = false;
    [SerializeField] private int remainingSegments = 0;

    public GameObject Owner { get; set; }

    public GameObject Target
    {
        get => target;
        set => target = value;
    }

    public bool Head
    {
        get => head;
        set => head = value;
    }

    private Coroutine coroutine;

    public int RemainingSegments
    {
        get => remainingSegments;
        set => remainingSegments = value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (segmentPrefab is null)
        {
            Debug.LogWarning("Tentacle segment prefab undefined.");
            Destroy(gameObject);
            return;
        }

        if (target is null)
        {
            Debug.LogWarning("Tentacle segment has no target.");
            Destroy(gameObject);
            return;
        }

        coroutine = StartCoroutine(ChaseTarget());
    }

    private IEnumerator ChaseTarget()
    {
        yield return new WaitForSeconds(segmentCreationTime);
        if (head && remainingSegments > 0)
        {
            head = false;
            var direction = (target.transform.position - transform.position).normalized;
            var position = transform.position + (direction * tentacleRange);
            var nextObject = Instantiate(segmentPrefab, position, Quaternion.identity);
            var nextSegment = nextObject.GetComponent<TentacleSegment>();
            nextSegment.head = true;
            nextSegment.remainingSegments = remainingSegments - 1;
            nextSegment.Owner = Owner;
        }

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!ReferenceEquals(other.gameObject, Owner) && other.gameObject.TryGetComponent<IMortal>(out IMortal mortal))
        {
            var tentacle = Instantiate(tentaclePrefab, other.gameObject.transform.position, Quaternion.identity);
            remainingSegments = 0;
            mortal.Die(DeathType.Default);
            Owner?.GetComponent<PlayerScore>().IncreaseKills();
        }
    }
}