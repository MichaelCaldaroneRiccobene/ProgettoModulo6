using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToOriginalPlace : MonoBehaviour, I_ITouch_Danger
{
    [Header("Setting")]
    [SerializeField] private GameObject mesh;
    [SerializeField] private GameObject poofVfx;

    [SerializeField] private float heightSpawn = 10;
    [SerializeField] private float speedForGoToThePoint = 2;

    private Vector3 startPos;
    private Vector3 startScale;
    private Quaternion startRot;

    private ParticleSystem particolPoof;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        startPos = transform.position;
        startRot = transform.rotation;
        startScale = transform.localScale;

        if(poofVfx != null )
        {
            GameObject particol = Instantiate(poofVfx, transform.position, Quaternion.identity, mesh.transform);
            particolPoof = particol.GetComponent<ParticleSystem>();
        }
    }

    public void Touch_Danger() => StartCoroutine(GoStartLocation());

    IEnumerator GoStartLocation()
    {
        Vector3 currentPos = transform.position;
        Vector3 endPos = startPos;
        Quaternion currentRot = transform.rotation;
        
        endPos.y += heightSpawn;
        rb.isKinematic = true;
        mesh.gameObject.SetActive(false);

        float progress = 0;

        while(progress < 1)
        {
            progress += Time.deltaTime * speedForGoToThePoint;
            Vector3 newPos = Vector3.Lerp(currentPos, endPos, progress);
            Quaternion newRot = Quaternion.Lerp(currentRot,startRot, progress);

            transform.position = newPos;
            transform.rotation = newRot;
            yield return null;
        }

        transform.localScale = startScale;
        rb.isKinematic = false;

        mesh.gameObject.SetActive(true);
        PlayPoofVFX();
    }

    private void PlayPoofVFX()
    {
        if(particolPoof == null) return;
        particolPoof.Play();
    }
}
