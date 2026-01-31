using UnityEngine;

public class BeamAbility : ActiveAbility
{
    [Header("Beam Settings")]
    public float distance = 3f;
    public float duration = 3f;
    public float elapsedTime = 0f;
    public bool activated = false;


    public override void Activate()
    {
        if (!activated) 
        {
            if (prefab == null)
            {
                Debug.LogError("BeamAbility prefab is NULL");
                return;
            }

            Vector2[] directions =
            {
            Vector2.up, Vector2.right,
            new Vector2(1,1).normalized, new Vector2(-1,1).normalized};

            foreach (var dir in directions)
            {
                SpawnBeam(dir);
            }
            activated = true;
        }
    }

    private void Update()
    {

        if (activated)
        {
            elapsedTime += Time.deltaTime;
        }

        if (elapsedTime >= duration)
        {
            Debug.Log($"E {elapsedTime}    D{duration}");
            this.gameObject.GetComponent<MaskEquip>().UnequipMask();
        }
    }

    void SpawnBeam(Vector2 direction)
    {
        GameObject beam = Instantiate(
            prefab,
            ownerTransform.position,
            Quaternion.identity,this.gameObject.transform
        );

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        beam.transform.rotation = Quaternion.Euler(0, 0, angle);

        beam.GetComponent<Beam>().SetOwner(gameObject);
    }
}
