using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : GenericSingleton<Game_Manager>  
{
    [Header("Level Setting")]
    [SerializeField] private int timeMinuts = 6;
    [SerializeField] private float timeSecond = 41;

    [Header("Take Important Stuff")]
    [SerializeField] private Transform targetTurret;

    private List<CheckPoint> checkPointList = new List<CheckPoint>();
    private List<GameObject> coinList = new List<GameObject>();

    private int coinTake;
    private int coinToT;

    private int currentMinuts;
    private float currentSecond;

    private bool isClockEnable;
    private bool isEndTime;

    public override void Awake() { base.Awake(); Time.timeScale = 1; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) if (PlayerUI.Instance != null && isClockEnable) PlayerUI.Instance.OpenMenu();
        Clock();
    }

    #region Clock
    private void Clock()
    {
        if (!isClockEnable) return;

        if(!isEndTime)
        {
            currentSecond -= Time.deltaTime;
            if (PlayerUI.Instance != null) PlayerUI.Instance.UpdateTime(currentMinuts, currentSecond);

            if (currentSecond <= 0)
            {
                currentSecond = 60;
                currentMinuts--;

                if (currentMinuts <= 0)
                {
                    isEndTime = true;
                    PlayerUI.Instance.OnLose();
                }
            }     
        }
    }

    public void EnableDisableClock(bool status) => isClockEnable = status;
    #endregion
    #region CheckPoints
    public void AddCheckPoint(CheckPoint checkPoint) => checkPointList.Add(checkPoint);
    public void RemoveCheckPoint(CheckPoint checkPoint) => checkPointList.Remove(checkPoint);

    public void CheckPointPress(CheckPoint point)
    {
        foreach (CheckPoint checkPoint in checkPointList) checkPoint.ChangeColor(Color.red);
        point.ChangeColor(Color.green);
    }
    #endregion
    #region Coins
    public void AddCoins(GameObject obj) => coinList.Add(obj);
    public void ClearCoins() { coinList.Clear(); Debug.Log("Coi9n Puliti"); }

    public void CoinTake()
    {
        coinTake++;
        if(PlayerUI.Instance != null) PlayerUI.Instance.UpdateCoin(coinTake,coinToT);
    }

    #endregion
    #region Turret
    public Transform GetTarget() => targetTurret;
    #endregion
    #region GameManager
    public void CanFinishGame()
    {
        if(coinTake < coinList.Count) return;

        if (PlayerUI.Instance != null && isClockEnable) PlayerUI.Instance.OnWin();

        coinList.Clear();
        Status();
    }
    public void Status()
    {
        isEndTime = false;
        isClockEnable = false;
        coinTake = 0;
        coinToT = coinList.Count;

        currentMinuts = timeMinuts;
        currentSecond = timeSecond;

        if (PlayerUI.Instance != null)
        {
            PlayerUI.Instance.UpdateTime(currentMinuts, currentSecond);
            PlayerUI.Instance.UpdateCoin(coinTake, coinToT);
        }
    }
    #endregion
}
