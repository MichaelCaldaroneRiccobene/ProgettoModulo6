using System;
using UnityEngine;

public class Ground_Check : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float raySphere = 0.2f;

    public Action<bool> onGround;

    private bool isGrounded;
    private bool currentOnGround;

    private void OnEnable()
    {
        currentOnGround = isGrounded;
    }

    private void Update() => IsOnGround();

    public void IsOnGround()
    {
        isGrounded = Physics.CheckSphere(transform.position, raySphere, groundLayer);

        if(currentOnGround != isGrounded)
        {
            currentOnGround = isGrounded;
            onGround?.Invoke(isGrounded);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, raySphere);
    }  
}
