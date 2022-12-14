using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class WindyRig : MonoBehaviour
{
    //public GameObject windSource;
    public float windIntensity;

    public GameObject IKTarget;
    private Vector3 origIKTargetPosition;

    private IKManager2D ikManager2D;

    void Start()
    {
        origIKTargetPosition = IKTarget.transform.localPosition;
        ikManager2D = GetComponent<IKManager2D>();
        ikManager2D.enabled = false;
    }

    void Update()
    {
        CameraManager cameraManager = FindObjectOfType<CameraManager>();
        Vector3 newIKTargetLocalPos = Vector3.zero;
        if (cameraManager.zoomTargetPosition != null && cameraManager.zoomTime < 1f) {
            // Target last zoom event 
            Vector3 target = new Vector3(
                cameraManager.zoomTargetPosition.x,
                cameraManager.zoomTargetPosition.y,
                0f
            );
            newIKTargetLocalPos = GetNewIKTargetLocalPos(target);
        } else {
            // Random sway
            Vector3 target = IKTarget.transform.position + new Vector3(
                Random.Range(-0.5f, 0.5f),
                Random.Range(-0.2f, 0.2f),
                0
            );
            target = (target * 7f + (origIKTargetPosition + transform.position)) / 8f;  // Make sure it doesn't get too far
            newIKTargetLocalPos = GetNewIKTargetLocalPos(target);
        }

        IKTarget.transform.localPosition = (IKTarget.transform.localPosition * 3f + newIKTargetLocalPos) / 4f;
    }

    Vector3 GetNewIKTargetLocalPos(Vector3 targetPosition) {
        Vector3 delta = (transform.position + origIKTargetPosition) - targetPosition;

        Vector3 scaledDelta = delta * windIntensity;
        Vector3 newIKTargetLocalPos = scaledDelta + origIKTargetPosition;

        return newIKTargetLocalPos;
    }

    void OnBecameVisible() {
        ikManager2D.enabled = true;
    }

    void OnBecameInvisible() {
        ikManager2D.enabled = true;
    }
}
