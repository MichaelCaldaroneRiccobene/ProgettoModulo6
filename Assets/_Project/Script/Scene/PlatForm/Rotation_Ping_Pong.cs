using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Ping_Pong : MonoBehaviour
{
    [Header("Setting Ping Pong")]
    [SerializeField] private float speedRotation = 5;
    [SerializeField] private bool isRandomSpeedRotation;

    [SerializeField] private float minSpeedRotation = 0.5f;
    [SerializeField] private float maxSpeedRotation = 3;

    [SerializeField] Quaternion startRot;
    [SerializeField] Quaternion newRot;


    private void Start()
    {
        if (isRandomSpeedRotation)
        {
            float factor = Random.Range(0, 2) == 0 ? -1 : 1;
            float randomSpeed = Random.Range(minSpeedRotation, maxSpeedRotation) * factor;
            speedRotation = randomSpeed;
        }
    }

    private void FixedUpdate()
    {
        float progression = Mathf.PingPong(Time.time * speedRotation, 1f);

        float smooth = Mathf.SmoothStep(0, 1, progression);
        Quaternion rot = Quaternion.Lerp(startRot, newRot, smooth);

        transform.rotation = rot;
    }
}
