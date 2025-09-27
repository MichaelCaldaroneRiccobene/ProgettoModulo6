using System.Collections;
using UnityEngine;

public class Turret_Animation_Color : MonoBehaviour, I_IDamage
{
    [Header("Logic Life")]
    [SerializeField] private int hitForDeth = 3;
    [SerializeField] private int timeDoFlickering = 5;
    [SerializeField] private float timeForFlickering = 0.1f;

    [Header("Get Mesh Render Turret")]
    [SerializeField] private MeshRenderer corpo;
    [SerializeField] private MeshRenderer occhio;

    private Color body;
    private Color eye;

    private bool isFlickering;

    private void Start()
    {
        body = corpo.material.color;
        eye = occhio.material.color;
    }

    public void Damage(int ammount)
    {
        hitForDeth--;
        if (hitForDeth <= 0) Destroy(gameObject);

        if (!isFlickering) StartCoroutine(OnHit());
    }

    private IEnumerator OnHit()
    {
        isFlickering = true;

        for(int i = 0; i < timeDoFlickering; i++)
        {
            corpo.material.color = Color.red;
            yield return new WaitForSeconds(timeForFlickering);
            corpo.material.color = body;
            yield return new WaitForSeconds(timeForFlickering);
        }
        isFlickering = false;
    }

    public void OnRangeTarget(bool onRangeTarget)
    {
        if (onRangeTarget) occhio.material.color = Color.red;
        else occhio.material.color = eye;
    }
}
