using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorController : MonoBehaviour
{
    public float speedTreshold = 0.1f;
    [Range(0, 1)]
    public float smoothing = 0.1f;

    private Animator animator;
    private Vector3 previosPos;
    private VRRIg vrRig;

    void Start()
    {
        animator = GetComponent<Animator>();
        vrRig = GetComponent<VRRIg>();
        previosPos = vrRig.head.vrTarget.position;
    }
    
    void Update()
    {
        Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previosPos) / Time.deltaTime;
        headsetSpeed.y = 0;

        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        previosPos = vrRig.head.vrTarget.position;

        float previusDirectionX = animator.GetFloat("DirectionX");
        float previusDirectionY = animator.GetFloat("DirectionY");

        animator.SetBool("isMoving", headsetLocalSpeed.magnitude > speedTreshold);
        animator.SetFloat("DirectionX", Mathf.Lerp(previusDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing));
        animator.SetFloat("DirectionY", Mathf.Lerp(previusDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing));
    }
}
