using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weed : MonoBehaviour
{
    public int health = 0;

    private float whiteTimer = 0f;
    private new SpriteRenderer renderer;

    void Start() {
        SetInitialHealth();
        renderer = GetComponent<SpriteRenderer>();
    }

    public virtual void DoAbility() {
    }

    private void TakeDamage(int damage) {
        whiteTimer = 0.05f;
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (whiteTimer > 0) {
            whiteTimer -= Time.deltaTime;
            renderer.color = Color.white;
        } else {
            renderer.color = new Color32(0xD4, 0x4C, 0x44, 0xFF);
        }

    }

    public void Hit(int damage) {
        TakeDamage(damage);
    }

    public abstract void SetInitialHealth();
}
