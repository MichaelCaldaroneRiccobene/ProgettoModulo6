using UnityEngine;
public class Player_Animation : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private string paramNameSpeed = "Speed";
    [SerializeField] private string paramNameDirection = "Direction";

    [SerializeField] private string paramNameAttack = "Attack";
    [SerializeField] private string paramNameRecover = "Recover";

    [SerializeField] private string paramNameLand = "Land";
    [SerializeField] private string paramNameIsGround = "IsGround";
    [SerializeField] private string paramNameJump = "Jump";
    [SerializeField] private string paramNameJumpAir = "JumpAirSpeed";

    [SerializeField] private float smoothLerpAnimation = 0.01f;
    [SerializeField] private float maxSpeedJumpAir = 1f;

    private Rigidbody rb;
    private Player_Input player_Input;
    private Ground_Check ground_Check;
    private Animator animator;

    private float SpeedJumpAir;

    private bool onGround;


    private void Start()
    {

    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
        ground_Check = transform.root.GetComponentInChildren<Ground_Check>();
        player_Input = GetComponentInParent<Player_Input>();
        onGround = true;

        SetUpAction();
    }

    private void Update()
    {
        SpeedJumpAir = Mathf.Max(0,SpeedJumpAir - maxSpeedJumpAir * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Movement();

        animator.SetFloat(paramNameJumpAir, SpeedJumpAir, smoothLerpAnimation, Time.fixedDeltaTime);
    }

    private void SetUpAction()
    {
        if(ground_Check != null) ground_Check.onGround += IsOnGround;

        if (player_Input != null)
        {
            player_Input.OnJumpAir += OnJumpAir;
            player_Input.OnJump += OnJump;
        }
    }

    private void Movement()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);

        float vertical = Mathf.Clamp(localVelocity.z / 4, -1f, 1f);

        float horizontal = Mathf.Clamp(localVelocity.x / 4, -1f, 1f);

        animator.SetFloat(paramNameSpeed, vertical, smoothLerpAnimation, Time.fixedDeltaTime);
        //animator.SetFloat(paramNameDirection, speedHorizontal);
    }

    public void OnAttack() => animator.SetTrigger(paramNameAttack);

    public void Recover() => animator.SetTrigger(paramNameRecover);

    public void IsOnGround(bool isOnGround)
    {
        if (onGround == isOnGround) return;

        onGround = isOnGround;

        animator.SetBool(paramNameIsGround, isOnGround);
        if(!isOnGround) animator.SetTrigger(paramNameLand);
    }

    public void OnJump() => animator.SetTrigger(paramNameJump);

    public void OnJumpAir() => SpeedJumpAir = maxSpeedJumpAir;

    private void OnDisable()
    {
        if (ground_Check != null) ground_Check.onGround -= IsOnGround;

        if (player_Input != null)
        {
            player_Input.OnJumpAir -= OnJumpAir;
            player_Input.OnJump -= OnJump;
        }
    }
}
