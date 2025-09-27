using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatForm_Sequence : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] Transform target;
    [SerializeField] private float speedMoving = 5;

    [SerializeField] private bool isSmooth;

    private Vector3 targetPos;
    private Vector3 currentPos;

    private float progress = 0f;

    private void Start()
    {
        targetPos = target.position;
        currentPos = transform.position;
    }

    private void FixedUpdate() => MovingInSequence();

    private void MovingInSequence()
    {
        if(target == null) return;
 
        progress += Time.fixedDeltaTime * speedMoving;
        float smooth = Mathf.SmoothStep(0, 1, progress);

        float t = isSmooth? smooth : progress;

        transform.position = Vector3.Lerp(currentPos, targetPos, t);

        if (progress >= 1)
        {
            progress = 0;

            currentPos = transform.position;
            targetPos = target.position;
        }
    }
}
