using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger_Object : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private int damage = 5;

    private void OnCollisionEnter(Collision collision)
    {
        I_IDamage i_IDamage = collision.collider.GetComponent<I_IDamage>();
        if (i_IDamage != null) i_IDamage.Damage(-damage);

        I_ITouch_Danger i_Touch_Water = collision.collider.gameObject.GetComponent<I_ITouch_Danger>();
        if (i_Touch_Water != null) i_Touch_Water.Touch_Danger();
    }
}
