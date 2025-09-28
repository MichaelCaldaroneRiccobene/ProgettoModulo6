using UnityEngine;

public class RotationNormal : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float rotationSpeed = 20;
    [SerializeField] private float minSpeedRotation = 0.5f;
    [SerializeField] private float maxSpeedRotation = 3;

    [SerializeField] private bool isRandomSpeedRotation;

    private void Start()
    {
        if (isRandomSpeedRotation)
        {
            float factor = Random.Range(0, 2) == 0 ? -1 : 1;
            float randomSpeed = Random.Range(minSpeedRotation, maxSpeedRotation) * factor;
            rotationSpeed = randomSpeed;
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, rotationSpeed * Time.fixedDeltaTime, 0);
    }
}
