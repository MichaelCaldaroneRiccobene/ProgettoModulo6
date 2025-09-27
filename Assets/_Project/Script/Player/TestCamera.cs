using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [Header("target da seguire")]
    public Transform target;

    [Header("Impostazioni distanza")]
    public float distance = 5f;
    public float minDistance = 2f;
    public float maxDistance = 8f;

    [Header("Rotazione")]
    public float mouseSensitivity = 3f;
    public float minVerticalAngle = -40f;
    public float maxVerticalAngle = 80f;

    [Header("Collisione")]
    public LayerMask collisionLayers;
    public float collisionRadius = 0.3f;
    public float smoothCollision = 10f;

    private float yaw;   // rotazione orizzontale
    private float pitch; // rotazione verticale
    private float currentDistance;

    private Vector3 desiredPosition;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("OrbitCamera: manca il target!");
            enabled = false;
            return;
        }

        currentDistance = distance;

        // inizializza angoli con rotazione attuale
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Input mouse
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);

        // Rotazione da input
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Calcola posizione desiderata dietro al target
        desiredPosition = target.position - (rotation * Vector3.forward * distance);

        // --- COLLISION DETECTION ---
        RaycastHit hit;
        if (Physics.SphereCast(target.position, collisionRadius, (desiredPosition - target.position).normalized, out hit, distance, collisionLayers))
        {
            currentDistance = Mathf.Lerp(currentDistance, hit.distance, Time.deltaTime * smoothCollision);
        }
        else
        {
            currentDistance = Mathf.Lerp(currentDistance, distance, Time.deltaTime * smoothCollision);
        }

        // Aggiorna posizione con distanza corretta
        Vector3 finalPosition = target.position - (rotation * Vector3.forward * currentDistance);
        transform.position = finalPosition;

        // Guarda sempre il target
        transform.LookAt(target.position);
    }

    private void OnDrawGizmos()
    {
        if (target == null) return;

        Gizmos.color = Color.yellow;
        // linea dal target alla camera
        Gizmos.DrawLine(target.position, transform.position);

        Gizmos.color = Color.cyan;
        // sfera che rappresenta il collisionRadius
        Gizmos.DrawWireSphere(transform.position, collisionRadius);

        Gizmos.color = Color.red;
        // posizione desiderata (senza collisione)
        Gizmos.DrawWireSphere(desiredPosition, 0.15f);
    }
}
