using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Setting For Walk/Run")]
    [SerializeField] private float speedWalk = 10;
    [SerializeField] private float maxWalkSpeed = 20;

    [SerializeField] private float speedRun = 25;
    [SerializeField] private float maxRunSpeed = 30;

    [SerializeField] private float maxSpeedForWalkAnimation = 0.5f;

    [Header("Setting Rotation Player")]
    [SerializeField] private float speedRotatingNormal = 5;
    [SerializeField] private float speedRotatingFocus = 10;

    [Header("Setting For Jump/JumpAir")]
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float jumpForceAir = 10;
    [SerializeField] private float timeForStayInAir = 2;

    [Header("Drag")]
    [SerializeField] private float dragGround = 5;
    [SerializeField] private float dragAir = 0.25f;

    private bool isRunning;
    private float movementSpeed;
    private float currentMaxMovementSpeed;
    private float lastTimeStayInAir;
    private float forceStayInAirOriginal;
    private float forceStayInAir;

    private Rigidbody rb;
    private Player_Input playerController;
    private Ground_Check ground_Check;

    private Vector3 direction;

    private bool isOnGround;
    private bool isCurrentOnGround;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<Player_Input>();
        ground_Check = GetComponentInChildren<Ground_Check>();

        SetupActions();

        isCurrentOnGround = isOnGround;

        forceStayInAirOriginal = jumpForceAir;
        forceStayInAir = forceStayInAirOriginal;
    }

    private void Update()
    {
        if(isCurrentOnGround !=  isOnGround)
        {  
            isCurrentOnGround = isOnGround;
            if(isOnGround)
            {
                rb.drag = dragGround;
                forceStayInAir = forceStayInAirOriginal;
                lastTimeStayInAir = 0;
            }
            else rb.drag = dragAir;
        }

        if(!isOnGround)
        {
            lastTimeStayInAir += Time.deltaTime;
            if (lastTimeStayInAir >= timeForStayInAir) forceStayInAir = 0;
        }
    }

    private void FixedUpdate() => Movement();

    private void SetupActions()
    {
        if(playerController != null)
        {
            playerController.OnDirectionMove += SetUpDirection;
            playerController.OnJump += Jump;
            playerController.OnJumpAir += JumpInAir;
            playerController.IsRunning += IsRunnig;
        }

        if (ground_Check != null) ground_Check.onGround += IsOnGround;
    }

    private void SetUpDirection(float horizontal, float vertical)
    {
        if (Camera.main == null) return;
        if (rb.isKinematic) return;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0; right.y = 0;

        forward.Normalize();right.Normalize();

        direction = forward * vertical + right * horizontal;
        if (direction.magnitude > 1) direction.Normalize();

        LogicRotationTarget(forward);
    }

    private void LogicRotationTarget(Vector3 forward)
    {
        if (direction.sqrMagnitude < 0.01f) return;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), speedRotatingFocus * Time.fixedDeltaTime);
    }

    public void Movement()
    {
        if (direction.sqrMagnitude < 0.01f) return;

        movementSpeed = isRunning ? speedRun : speedWalk;
        currentMaxMovementSpeed = isRunning ? maxRunSpeed : maxWalkSpeed;

        Vector3 horizontalVelocityRb = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        Vector3 clampVelocity;
        if (horizontalVelocityRb.magnitude > currentMaxMovementSpeed)
        {
            clampVelocity = horizontalVelocityRb.normalized * currentMaxMovementSpeed;
            rb.velocity = new Vector3(clampVelocity.x, rb.velocity.y, clampVelocity.z);
            return;
        }
        rb.AddForce(direction.normalized * movementSpeed, ForceMode.Force);
    }

    private void IsOnGround(bool isOnGround) => this.isOnGround = isOnGround;

    private void IsRunnig(bool isRunning) => this.isRunning = isRunning;

    private void Jump() 
    {
        if (isOnGround) rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        rb.drag = dragAir;
    }

    private void JumpInAir() 
    { 
        if (!isOnGround) rb.AddForce(Vector3.up * forceStayInAir, ForceMode.VelocityChange); 
    }

    private void OnDisable()
    {
        if (playerController != null)
        {
            playerController.OnDirectionMove -= SetUpDirection;
            playerController.OnJump -= Jump;
            playerController.OnJumpAir -= JumpInAir;
            playerController.IsRunning -= IsRunnig;
        }

        if (ground_Check != null) ground_Check.onGround -= IsOnGround;
    }
}
