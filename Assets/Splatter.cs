using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Splatter : MonoBehaviour
{

    void Start()
    {
        Texture2D[] splatterTexs = PrefabsManager.getInstance().splatterTexs;
        int r = (int)Random.Range(0, splatterTexs.Length);
        Texture2D splatterTex = splatterTexs[r];

        Material mat = GetComponent<DecalProjector>().material;
        mat.SetTexture("_Base_Texture", splatterTex);
        //mat.SetColor("_Base_Color", Color.red);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
