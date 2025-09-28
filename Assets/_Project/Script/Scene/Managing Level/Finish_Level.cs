using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Transform bigWall;
    [SerializeField] private float newHeightWall;

    private void OnTriggerEnter(Collider other)
    {
        Player_Input player = other.GetComponent<Player_Input>();
        if (player != null) { if (Game_Manager.Instance != null) Game_Manager.Instance.CanFinishGame(); }
    }
}
