using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PannelType { None,Menu, Option, WinScreen, LoseScreen }

[System.Serializable]
public class TypeOfPannelMenu
{
    public GameObject Pannel;
    public PannelType Type;
}

public class PlayerUI : GenericSingleton<PlayerUI>  
{
    [Header("Setting UI Player")]
    [SerializeField] private Image imageHp;
    [SerializeField] private Image crown;

    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private TextMeshProUGUI textCoin;

    [Header("Setting Menu Player")]
    [SerializeField] private List<TypeOfPannelMenu> pannels;

    [Header("Buttons")]
    [SerializeField] private Button resume;
    [SerializeField] private Button options;
    [SerializeField] private Button[] goToMenu;
    [SerializeField] private Button quit;
    [SerializeField] private Button returnBack;
    [SerializeField] private Button rePlay;

    private bool isMenuPlayerOpen;

    private void Start()
    {
        SetPannel(PannelType.None);
        SetUpButtons();
    }

    private void SetPannel(PannelType panelType)
    {
        if (pannels.Count < 0) return;

        foreach (TypeOfPannelMenu pannel in pannels)
        {
            if (pannel.Type == panelType) pannel.Pannel.SetActive(true);
            else pannel.Pannel.SetActive(false);
        }
    }

    #region PannelsMenu
    private void SetUpButtons()
    {
        if (resume != null) resume.onClick.AddListener(OpenMenu);
        if (options != null) options.onClick.AddListener(GoOption);

        if (goToMenu.Length > 0) foreach (Button button in goToMenu) button.onClick.AddListener(ReturnToMenu);

        if (quit != null) quit.onClick.AddListener(OnQuit);
        if (returnBack != null) returnBack.onClick.AddListener(GoMainPannel);
        if (rePlay != null) rePlay.onClick.AddListener(RePlay);
    }

    public void OpenMenu()
    {
        isMenuPlayerOpen = !isMenuPlayerOpen;
        SetPannel(PannelType.Menu);

        if (isMenuPlayerOpen)
        {
            CursorMode(CursorLockMode.None, true);
            Time.timeScale = 0;
        }
        else ResumeGame();
    }
    public void ResumeGame()
    {
        isMenuPlayerOpen = false;
        Time.timeScale = 1;

        CursorMode(CursorLockMode.Locked, false);
        SetPannel(PannelType.None);
    }

    public void ReturnToMenu()
    {
        Debug.Log("Try");
        isMenuPlayerOpen = false;
        Time.timeScale = 1;
        SetPannel(PannelType.None);
        CursorMode(CursorLockMode.None, true);

        Debug.Log("Try2");

        if (ManagerLevel.Instance != null) ManagerLevel.Instance.ReturnToMainMenu();
        Debug.Log("Try3");
        if (Game_Manager.Instance != null) Game_Manager.Instance.ClearCoins();
        Debug.Log("Try4");
    }

    public void GoOption() => SetPannel(PannelType.Option);

    public void GoMainPannel() => SetPannel(PannelType.Menu);

    public void TheGameCanFinish(bool canFinish)
    {
        textCoin.gameObject.SetActive(!canFinish);
        crown.gameObject.SetActive(canFinish);
    }

    public void OnWin()
    {
        SetPannel(PannelType.WinScreen);
        Time.timeScale = 0;
        CursorMode(CursorLockMode.None, true);
    }

    public void OnLose()
    {
        SetPannel(PannelType.LoseScreen);
        Time.timeScale = 0;
        CursorMode(CursorLockMode.None, true);
    }
    private void OnQuit() => Application.Quit();

    private void CursorMode(CursorLockMode typeLockMode, bool isVisible)
    {
        Cursor.lockState = typeLockMode;
        Cursor.visible = isVisible;
    }
    #endregion

    public void UpdateLife(int hp,int maxHp)
    {
        Debug.Log("hp " + hp);
        imageHp.fillAmount = (float)hp / maxHp;
    }

    public void UpdateTime(int minuts,float seconds) => textTime.SetText(string.Format("{0:00}:{1:00}", minuts, (int)seconds));

    public void UpdateCoin(int coinTake, int coinToT)
    {
        textCoin.SetText(string.Format("{0}/{1}", coinTake, coinToT));
        Debug.Log("Coin " + coinTake + " ToT Coin " + coinToT);

        if(coinTake >= coinToT) TheGameCanFinish(true);
        else TheGameCanFinish(false);
    }

    private void RePlay() 
    {
        if (ManagerLevel.Instance != null) ManagerLevel.Instance.ReTryPlay(); 
        Time.timeScale = 1;
        SetPannel(PannelType.None);
    } 
}
