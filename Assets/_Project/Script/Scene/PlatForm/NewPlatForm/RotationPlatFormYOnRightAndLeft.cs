using UnityEngine;

public class RotationPlatFormYOnRightAndLeft : PlatForm
{
    [Header("Setting")]
    [SerializeField] private float rotationSpeed = 20;
    [SerializeField] private float minSpeedRotation = 0.5f;
    [SerializeField] private float maxSpeedRotation = 3;

    [SerializeField] private bool isRandomSpeedRotation;

    [SerializeField] private bool isRotationOnX;
    [SerializeField] private bool isRotationOnY;
    [SerializeField] private bool isRotationOnZ;

    public override void Start()
    {
        base.Start();
        if (isRandomSpeedRotation)
        {
            float factor = Random.Range(0, 2) == 0 ? -1 : 1;
            float randomSpeed = Random.Range(minSpeedRotation, maxSpeedRotation) * factor;
            rotationSpeed = randomSpeed;
        }
        rotationSpeedPlatForm = rotationSpeed;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (isRotationOnX) transform.Rotate(rotationSpeedPlatForm * Time.fixedDeltaTime, 0, 0);
        if (isRotationOnY)transform.Rotate(0, rotationSpeedPlatForm * Time.fixedDeltaTime, 0);
        if (isRotationOnZ) transform.Rotate(0, 0, rotationSpeedPlatForm * Time.fixedDeltaTime);
    }
}
