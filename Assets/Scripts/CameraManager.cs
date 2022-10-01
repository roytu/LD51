using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject followTarget;

    private Vector3 origDelta;


    void Start() {
        origDelta = transform.position;
    }
    public void SetFollowTarget(GameObject target) {
        followTarget = target;
    }

    void Update()
    {
        if (followTarget) {
            Vector3 followPosition = followTarget.transform.position;
            transform.position = followPosition + origDelta;
        }
    }
}
