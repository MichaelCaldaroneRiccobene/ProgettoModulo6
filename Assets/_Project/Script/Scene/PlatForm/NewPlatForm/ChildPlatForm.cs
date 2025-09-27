using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPlatForm : MonoBehaviour
{
    [SerializeField] private GameObject platformParent;

    private PlatForm platForm;
    private void Start()
    {
        if (transform.parent != null && platformParent == null) platForm = transform.parent.GetComponentInChildren<PlatForm>();
        if(platformParent != null) platForm = platformParent.GetComponent<PlatForm>();

        if (platForm != null) Debug.Log("Ho PLatForm");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb)) platForm.TemponaryRb = rb;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb)) platForm.TemponaryRb = null;
    }
}
