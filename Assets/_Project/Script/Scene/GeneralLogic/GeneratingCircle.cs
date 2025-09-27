using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratingCircle : MonoBehaviour
{
    [SerializeField] private GameObject preFab;
    [SerializeField] private Transform parentPreFabs;

    [SerializeField] private int count = 50;
    [SerializeField] private float distanceSpace = 10;

    [SerializeField] private float startYPosition;
    [SerializeField] private float upgradeYRotation = -15;
    [SerializeField] private float upgradeHeight = 2;
    [SerializeField] private float radius = 5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine(GeneratingRoutine());
    }

    private IEnumerator GeneratingRoutine()
    {
        float circumference = Mathf.PI * 2 * radius;
        int distance = Mathf.FloorToInt(circumference / distanceSpace);

        Vector3 pos = new Vector3 (0, startYPosition, 0);
        float y = pos.y;
        Vector3 targetRotation = new Vector3 (0, 0, 0);

        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2 / distance;

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius; 

            Vector3 position = new Vector3(x, y, z) + transform.position;
            Quaternion rotation = Quaternion.Euler(targetRotation);

            GameObject obj = Instantiate(preFab, position, rotation);
            if(parentPreFabs != null) obj.transform.parent = parentPreFabs.transform;

            targetRotation = new Vector3(0, targetRotation.y + upgradeYRotation, 0);
            y += upgradeHeight;

            if (obj.TryGetComponent(out ConfigurableJoint configurableJoint)) configurableJoint.connectedBody = rb;
            yield return null;
        }
    }
}
