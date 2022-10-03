using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    public Sprite[] sprites;

    public float minSize;
    public float maxSize;

    public Color color1;
    public Color color2;

    void Start()
    {
        // Choose a sprite randomly
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        int r = (int)Random.Range(0, sprites.Length);
        renderer.sprite = sprites[r];

        // Choose a size randomly
        float s = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * s;

        // Choose a color
        float interp = Random.Range(0f, 1f);
        renderer.color = Color32.Lerp(color1, color2, interp);
    }
}
