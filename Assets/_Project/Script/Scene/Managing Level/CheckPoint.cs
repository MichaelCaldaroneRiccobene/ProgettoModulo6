using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Renderer reder;

    private void OnEnable()
    {
        if (Game_Manager.Instance != null) Game_Manager.Instance.AddCheckPoint(this);
        reder = GetComponentInChildren<Renderer>();
        reder.material.color = Color.red;
    }

    public void ChangeColor(Color color) => reder.material.color = color;

    private void OnTriggerEnter(Collider other)
    {
        Player_Return_To_CheckPoint player_Return_To_CheckPoint = other.GetComponent<Player_Return_To_CheckPoint>();
        if(player_Return_To_CheckPoint != null )
        {
            player_Return_To_CheckPoint.PosLastCheckPoint = transform.position;
            player_Return_To_CheckPoint.RotLastCheckPoint = transform.rotation;

            if (Game_Manager.Instance != null) Game_Manager.Instance.CheckPointPress(this);
        }
    }

    private void OnDisable() { if (Game_Manager.Instance != null) Game_Manager.Instance.RemoveCheckPoint(this); }
}
