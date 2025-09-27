using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatForm_Ping_Pong : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float speedMoving = 5;
    [SerializeField] private bool isRandomSpeedMoving;

    [SerializeField] private float minSpeedMoving = 0.5f;
    [SerializeField] private float maxSpeedMoving = 3;

    [Header("Setting Ping Pong")]
    [SerializeField] Vector3 newPos;

    private Vector3 startPos;

    private void Start()
    {
        if (isRandomSpeedMoving)
        {
            float factor = Random.Range(0, 2) == 0 ? -1 : 1;
            float randomSpeed = Random.Range(minSpeedMoving, maxSpeedMoving) * factor;
            speedMoving = randomSpeed;
        }
        startPos = transform.position;
        newPos += startPos;
    }

    private void FixedUpdate()
    {
        float progression = Mathf.PingPong(Time.time * speedMoving, 1f);

        float smooth = Mathf.SmoothStep(0, 1, progression);
        Vector3 pos = Vector3.Lerp(startPos, newPos, smooth);

        transform.position = pos;
    }
}