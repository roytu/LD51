using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage;
    public float lifetime;
    public bool isOneHit;
    public Vector2 knockbackDirection;
    public float knockbackAmount = 0;

    private float t = 0;

    void Start()
    {
        t = 0;
    }

    void Update()
    {
        t += Time.deltaTime;
        if (t >= lifetime) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.gameObject.CompareTag("Weed")) {
            Weed weed = otherCollider.gameObject.GetComponent<Weed>();
            weed.Hit(damage);
            if (knockbackAmount > 0) {
                if (knockbackDirection == null) {
                    float r = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
                    knockbackDirection = new Vector2(
                        Mathf.Cos(r),
                        Mathf.Sin(r)
                    );
                }
                // Apply force
                Vector2 force = knockbackDirection.normalized * knockbackAmount;
                weed.rigidbody.AddForce(force, ForceMode2D.Impulse);

                // Add screenshake
                CameraManager cameraManager = FindObjectOfType<CameraManager>();
                cameraManager.AddScreenshake(knockbackAmount * 0.01f);
            }
            if (isOneHit) {
                Destroy(gameObject);
            }
        }
    }


}
