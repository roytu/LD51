using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    public Sprite[] sprites;

    void Start()
    {
        // Get sprite renderer
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        // Choose a sprite randomly
        int r = (int)Random.Range(0, sprites.Length);

        renderer.sprite = sprites[r];
    }
}
