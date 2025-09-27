using UnityEngine;

public class Moving_PlatForm : MonoBehaviour
{
    public Vector3 VelocityPlatForm {  get; private set; }
    public Quaternion RotationPlatForm {  get; private set; }

    private Vector3 previousPosition;
    private Quaternion lastRotation;

    private void Start()
    {
        previousPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        VelocityPlatForm = transform.position - previousPosition;
        RotationPlatForm = transform.rotation * Quaternion.Inverse(lastRotation);

        previousPosition = transform.position;
        lastRotation = transform.rotation;
    }
}
