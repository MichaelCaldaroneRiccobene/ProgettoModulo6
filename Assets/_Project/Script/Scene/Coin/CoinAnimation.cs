using System.Collections;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    [Header("Setting Animation Coin")]
    [SerializeField] private Transform refCoin;
    [SerializeField] private float minScale = 0.2f;
    [SerializeField] private float timeInterpolateMinScale = 1.5f;

    private Vector3 oriScale;

    private void Start()
    {
        StartCoroutine(ConserveCoinScaleRoutine());
    }

    public void AnimationTakeCoin() => StartCoroutine(OnAnimationTakeCoinRoutine());

    private IEnumerator ConserveCoinScaleRoutine()
    {
        // Ho Provato a Fare in altri modi ma questo metodo funziona per mantenere la scale del coin
        // Per Ora lo mantengo cosi se avro tempo cerco di migliorarlo 

        yield return new WaitForSeconds(1);
        {
            oriScale = refCoin.transform.localScale;
            Vector3 minimalScale = new Vector3(minScale, minScale, minScale);

            float progress = 0;
            while (progress < 1)
            {
                progress += Time.deltaTime;
                float smooth = Mathf.SmoothStep(0, 1, progress);

                refCoin.transform.localScale = Vector3.Lerp(oriScale, minimalScale, smooth);
                yield return null;
            }

            progress = 0;

            while (progress < 1)
            {
                progress += Time.deltaTime;
                float smooth = Mathf.SmoothStep(0, 1, progress);

                refCoin.transform.localScale = Vector3.Lerp(minimalScale, oriScale, smooth);
                yield return null;
            }

        }
    }

    private IEnumerator OnAnimationTakeCoinRoutine()
    {
        oriScale = refCoin.transform.localScale;
        Vector3 minimalScale = new Vector3(minScale, minScale, minScale);

        float progress = 0;
        while (progress < 1)
        {
            progress += Time.deltaTime * timeInterpolateMinScale;
            float smooth = Mathf.SmoothStep(0,1,progress);

            refCoin.transform.localScale = Vector3.Lerp(oriScale, minimalScale, smooth);
            yield return null;
        }

        //gameObject.SetActive(false);
    }

    private void OnDisable() => refCoin.transform.localScale = oriScale;
}
