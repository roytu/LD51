using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopoWhackerProjectile : MonoBehaviour
{

    public Vector2 direction;
    public float speed;


    private float t = 0;
    private float timeTillNextHitbox = 0f;
    private float DELAY_TILL_HITBOX = 0.2f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the "mole"
        Vector2 moleVelocity = direction.normalized * speed * Time.fixedDeltaTime;
        transform.position += new Vector3(
            moleVelocity.x,
            moleVelocity.y,
            0
        );

        // Spawn hitboxes
        timeTillNextHitbox -= Time.fixedDeltaTime;
        if (timeTillNextHitbox <= 0f) {
            SpawnHitbox();

            timeTillNextHitbox = DELAY_TILL_HITBOX;
        }

    }

    private void SpawnHitbox() {
        GameObject hitboxGO = GameObject.Instantiate(PrefabsManager.getInstance().topoWhackerProjectileHitboxPrefab, transform.position, Quaternion.identity);
        Hitbox hitbox = hitboxGO.GetComponent<Hitbox>();
        float r = Random.Range(0, 360) * Mathf.Deg2Rad;
        hitbox.knockbackDirection = new Vector2(
            Mathf.Cos(r),
            Mathf.Sin(r)
        );
        hitbox.knockbackAmount = 10f;
    }


}
