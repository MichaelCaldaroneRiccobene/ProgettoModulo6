using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public enum LevelMapType { None,One,Two }

[System.Serializable]
public class LevelMap
{
    public GameObject Map;
    public LevelMapType Type;
}

public class NewManagerMenu : GenericSingleton<NewManagerMenu>
{
    [SerializeField] private GameObject pannelMenu;

    [Header("Setting Menu")]
    [SerializeField] private List<TypeOfPannelMenu> pannels;
    [SerializeField] private List<LevelMap> levelMaps;

    [SerializeField] private Animator animatorPlayerMenu;

    [Header("Setting Buttons")]
    [SerializeField] private Button onPlayMapOne;
    [SerializeField] private Button onPlayMapTwo;
    [SerializeField] private Button onOption;
    [SerializeField] private Button onBack;
    [SerializeField] private Button onQuit;

    private bool isGoOnTheLevel;
    private bool isOnPlayMode;

    private void Start() => SetUP();

    #region SetUp

    private void SetUP()
    {
        SetPannel(PannelType.Menu);
        SetMap(LevelMapType.None);

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

    private void SetMap(LevelMapType mapType)
    {
        if (levelMaps.Count < 0) return;

        foreach (LevelMap map in levelMaps)
        {
            if (map.Type == mapType) map.Map.SetActive(true);
            else map.Map.SetActive(false);
        }
    }
    private void SetUpButtons()
    {
        if (onPlayMapOne != null) onPlayMapOne.onClick.AddListener(OnPlayMapOne);
        if (onPlayMapTwo != null) onPlayMapTwo.onClick.AddListener(OnPlayMapTwo);

        if (onOption != null) onOption.onClick.AddListener(OnOption);

        if (onBack != null) onBack.onClick.AddListener(OnBack);
        if (onQuit != null) onQuit.onClick.AddListener(OnQuit);
    }


    #endregion
    #region LogicOnPlay
    public void OnPlayMapOne() => OnPlay(LevelMapType.One);

    public void OnPlayMapTwo() => OnPlay(LevelMapType.Two);

    private void OnPlay(LevelMapType type)
    {
        Debug.Log("TryTo GoLevel " + type);
        if (isGoOnTheLevel || isOnPlayMode) return;

        isOnPlayMode = true;
        isGoOnTheLevel = true;

        Debug.Log("GoLevel " + type);
        LevelMap map = levelMaps.Find(a => a.Type == type);
        if (map == null) Debug.LogError("Map Not Find");

        if (ManagerLevel.Instance != null) ManagerLevel.Instance.OnPlay(map.Map);
        animatorPlayerMenu.SetTrigger("Fall");
    }

    public bool CanReturnToMainMenu() => isGoOnTheLevel;
    public void IsOnTheLevel() => isGoOnTheLevel = false;
    #endregion
    #region LogicMenu
    private void OnOption() => SetPannel(PannelType.Option);
    private void OnBack() => SetPannel(PannelType.Menu);
    private void OnQuit() => Application.Quit();
    #endregion


    public void OnReturnToMainMenu() => StartCoroutine(UnAliveLevelsRoutine());

    private IEnumerator UnAliveLevelsRoutine()
    {
        Debug.Log("ReturnMenuNew");
        isGoOnTheLevel = true;
        yield return new WaitForSeconds(2);

        SetMap(LevelMapType.None);
        isOnPlayMode = false;
        isGoOnTheLevel = false;
    }
}
