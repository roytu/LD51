using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Splatter : MonoBehaviour
{

    public Color color1;
    public Color color2;

    private Color origColor;

    private float t = 0;
    private Material mat;

    void Start()
    {
        // Duplicate material
        DecalProjector decalProjector = gameObject.GetComponent<DecalProjector>();
        Material origMat = decalProjector.material;
        mat = new Material(origMat);
        decalProjector.material = mat;

        // Set texture
        Texture2D[] splatterTexs = PrefabsManager.getInstance().splatterTexs;
        int r = (int)Random.Range(0, splatterTexs.Length);
        Texture2D splatterTex = splatterTexs[r];

        // Set color
        float rc = Random.Range(0f, 1f);
        origColor = Color.Lerp(color1, color2, rc);

        mat.color = origColor;
        mat.mainTexture = splatterTex;

        // Randomize rotation
        transform.localEulerAngles = new Vector3(0, 0, Random.Range(0f, 360f) * Mathf.Deg2Rad);

        // Randomize scale
        transform.localScale *= Random.Range(0.8f, 1.2f);

        // Initialize time
        t = 0;
    }

    void Update()
    {
        t += Time.deltaTime;

        // Darken colors/opacity over time
        float h, s, v;
        Color.RGBToHSV(origColor, out h, out s, out v);
        v *= Mathf.Max((1f - (t / 30f) * 0.6f), 0.5f);
        Color color = Color.HSVToRGB(h, s, v);
        color.a = Mathf.Max((1f - (t / 100f) * 0.8f), 0.1f);

        mat.color = color;

        // Lifetime control
        if (t > 100) {
            Destroy(gameObject);
        }
        
    }
}
