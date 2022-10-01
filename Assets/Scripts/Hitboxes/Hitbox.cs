using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage;
    public float lifetime;
    public bool isOneHit;

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
        Debug.Log(otherCollider);
        if (otherCollider.gameObject.CompareTag("Weed")) {
            Weed weed = otherCollider.gameObject.GetComponent<Weed>();
            weed.Hit(damage);
            if (isOneHit) {
                Destroy(gameObject);
            }
        }
    }


}
