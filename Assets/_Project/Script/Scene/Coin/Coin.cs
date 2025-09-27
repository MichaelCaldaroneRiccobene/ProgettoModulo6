using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public UnityEvent AnimationTakeCoin;

    private bool isCoinTake = false;

    private void OnEnable() => EnableCoin();

    private void EnableCoin()
    {
        if (Game_Manager.Instance != null) Game_Manager.Instance.AddCoins(gameObject); 
        isCoinTake = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        Player_Input player_Controller = other.GetComponent<Player_Input>();

        if(player_Controller != null) OnTakeCoin();
    }

    [ContextMenu("OnTakeCoin")]
    private void OnTakeCoin()
    {
        if(isCoinTake) return;

        isCoinTake = true;
        if (Game_Manager.Instance != null) Game_Manager.Instance.CoinTake();
        AnimationTakeCoin?.Invoke();
    }

    //private void OnDisable() { if (Game_Manager.Instance != null) Game_Manager.Instance.RemoveCoins(gameObject); }
}
