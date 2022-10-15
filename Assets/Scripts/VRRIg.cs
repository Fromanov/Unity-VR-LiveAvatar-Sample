using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackinfPositionOffset;
    public Vector3 trackinfRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackinfPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackinfRotationOffset);
    }
}

public class VRRIg : MonoBehaviour
{
    public VRMap head;
    public VRMap rightHand;
    public VRMap leftHand;

    public Transform headConstraint;
    public Vector3 headBodyOffset;
    public float turnSmothness;

    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }
    
    void FixedUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward,
            Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.deltaTime * turnSmothness);

        head.Map();
        rightHand.Map();
        leftHand.Map();
    }
}
