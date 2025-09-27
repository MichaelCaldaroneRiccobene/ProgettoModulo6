using System;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    public Action OnMenu;

    public Action<float, float> OnDirectionLook;

    public Action<float,float> OnDirectionMove;
    public Action<bool> IsOnFocus;
    public Action<bool> IsRunning;

    public Action OnJump;
    public Action OnJumpAir;

    public Action OnAttack;

    private float horizontal;
    private float vertical;

    private float mouseInputX;
    private float mouseInputY;

    private bool isFocusMode;
    private bool onDisableInput;

    private Player_Return_To_CheckPoint player_Return;
    private Life_Controller life_Controller;

    private void OnEnable()
    {
        if (player_Return == null) player_Return = GetComponent<Player_Return_To_CheckPoint>();
        if (life_Controller == null) life_Controller = GetComponent<Life_Controller>();

        SetUpAction();
    }

    private void Update() => InputPlayer();

    private void FixedUpdate()
    {
        if (onDisableInput) return;
        if (Input.GetKey(KeyCode.Space)) OnJumpAir?.Invoke();
    }

    private void SetUpAction()
    {
        if (player_Return != null) player_Return.onDisableInput += OnDisableInput;

        if(life_Controller != null)
        {
            if(PlayerUI.Instance != null) life_Controller.OnLFChange += PlayerUI.Instance.UpdateLife;
            life_Controller.OnDeath += OnDead;
        }
    }

    private void InputPlayer()
    {
        //Menu
        if (Input.GetKeyDown(KeyCode.Escape)) OnMenu?.Invoke();

        if (onDisableInput)
        {
            IsRunning?.Invoke(false);
            return;
        }

        //Move
        horizontal = Input.GetAxis("Horizontal"); vertical = Input.GetAxis("Vertical");
        OnDirectionMove?.Invoke(horizontal, vertical);;

        //Look
        mouseInputX = Input.GetAxis("Mouse X"); mouseInputY = Input.GetAxis("Mouse Y");
        OnDirectionLook?.Invoke(mouseInputX, mouseInputY);;

        // Jump And JumpAir
        if (Input.GetKeyDown(KeyCode.Space)) OnJump?.Invoke();

        // Camera
        if (Input.GetMouseButtonDown(1))
        {
            isFocusMode = true;
            IsOnFocus?.Invoke(isFocusMode);
        }

        if (Input.GetMouseButtonUp(1))
        {
            isFocusMode = false;
            IsOnFocus?.Invoke(isFocusMode);
        }
        //

        //Shooting
        if (Input.GetMouseButtonDown(0)) OnAttack?.Invoke();

        //Runnig
        if (Input.GetKeyDown(KeyCode.LeftShift)) IsRunning?.Invoke(true);
        if (Input.GetKeyUp(KeyCode.LeftShift)) IsRunning?.Invoke(false);
    }

    private void OnDead() { if (PlayerUI.Instance != null) PlayerUI.Instance.OnLose(); }

    private void OnDisableInput(bool onDisableInput)
    {
        isFocusMode = false;
        IsOnFocus?.Invoke(isFocusMode);
        this.onDisableInput = onDisableInput;
    }

    private void OnDisable()
    {
        if (player_Return != null) player_Return.onDisableInput -= OnDisableInput;

        if (life_Controller != null)
        {
            if (PlayerUI.Instance != null) life_Controller.OnLFChange -= PlayerUI.Instance.UpdateLife;
            life_Controller.OnDeath -= OnDead;
        }
    }
}
