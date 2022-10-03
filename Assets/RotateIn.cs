using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateIn : MonoBehaviour
{
    private RectTransform rectTransform;
    public float targetRotation;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float rotation = rectTransform.transform.localEulerAngles.z;
        float dr = (rotation - targetRotation + 360) % 360;
        if (dr > 180f) {
            dr -= 360f;
        }
        rotation = dr + targetRotation;
        rotation = (rotation * 15f + targetRotation) / 16f;
        rectTransform.transform.localEulerAngles = new Vector3(0, 0, rotation);
        
    }
}
