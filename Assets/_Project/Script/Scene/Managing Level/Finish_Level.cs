using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FinishLevel : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Transform bigWall;
    [SerializeField] private float newHeightWall;
    [SerializeField] private float speedLerp = 1;

    public UnityEvent onWin;

    private bool canFinishLevel;

    private void OnTriggerEnter(Collider other)
    {
        Player_Input player = other.GetComponent<Player_Input>();
        if (player != null) { if (Game_Manager.Instance != null) Game_Manager.Instance.CanFinishGame(); }
    }

    public void OnFinishLevel()
    {
        canFinishLevel = true;
        Vector3 oriPos = bigWall.position;
        oriPos.y += newHeightWall;

        StartCoroutine(WallUpAnimation(oriPos));
    }

    private IEnumerator WallUpAnimation(Vector3 newPos)
    {
        Vector3 startPos = bigWall.position;
        float progress = 0;

        while (progress < 1)
        {
            progress += Time.deltaTime * speedLerp;
            bigWall.position = Vector3.Lerp(startPos, newPos, progress);

            yield return null;
        }
    }
}
