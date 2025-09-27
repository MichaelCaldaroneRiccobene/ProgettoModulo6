using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormVisual : MonoBehaviour
{
    [SerializeField] private GameObject roof;
    [SerializeField] private bool hasRoof;

    private void Start() => SetRoof();

    [ContextMenu("SetRoof")]
    private void SetRoof() => roof.SetActive(hasRoof);
}
