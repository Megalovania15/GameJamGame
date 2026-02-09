using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Totem : MonoBehaviour
{
    public Transform shootPoint;

    public GameObject Owner { get; set; }

    [SerializeField]
    private float fireRate = 0.5f;
    [SerializeField]
    private float lifetime = 10f;
    [SerializeField]
    private float shootDelay = 1f;
    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private GameObject bulletPrefab;

    private List<GameObject> targets = new();
    private Transform currentTarget = null;
    private Coroutine findNewTarget;

    void Awake()
    {
        StartCoroutine(DestroyObject());
    }

    //I can move the targeting logic to a separate class later so we can
    // reuse it. For now it will live here
    Transform SelectTarget()
    {
        if (targets.Count > 0)
        {
            currentTarget = targets[0].transform;
            return currentTarget;
        }

        return null;
    }

    Quaternion RotateToTarget()
    {
        if (currentTarget != null)
        {
            var lookPos = currentTarget.position - transform.position;

            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;

            var newRotation = Quaternion.Euler(0, 0, angle);

            return newRotation;
        }

        return shootPoint.rotation;
    }

    IEnumerator FindNewTarget()
    {
        yield return new WaitForSeconds(0.1f);
        var newTarget = SelectTarget();
        if (newTarget != null)
        {
            currentTarget = newTarget;
            StartCoroutine(Rotate());
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Rotate()
    {
        float step = rotationSpeed * Time.deltaTime;

        while (currentTarget is not null && Vector3.Distance(
            transform.position, currentTarget.position) > 0.1f)
        {
            var newRotationPos = RotateToTarget();

            shootPoint.rotation = Quaternion.Lerp(
                shootPoint.rotation, newRotationPos, step);

            yield return null;
        }

    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootDelay);

        while (currentTarget != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.GetComponent<Bullet>().SetOwner(Owner);
            yield return new WaitForSeconds(fireRate);
        }
    }

    //could have an animation or effect that occurs when this gets destroyed
    IEnumerator DestroyObject()
    {
        yield return new WaitForSecondsRealtime(lifetime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!ReferenceEquals(collision.gameObject, Owner) && 
                !targets.Contains(collision.gameObject))
            {
                targets.Add(collision.gameObject);
                findNewTarget = StartCoroutine(FindNewTarget());
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (targets.Contains(collision.gameObject))
            {
                targets.Remove(collision.gameObject);
                StopCoroutine(findNewTarget);
            }

            if (targets.Count == 0)
            {
                currentTarget = null;
            }
        }
    }
}
