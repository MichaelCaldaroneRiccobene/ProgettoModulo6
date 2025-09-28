using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public enum TypeOfPlayer { OnMenu,OnFall,OnPlay}

[Serializable]
public class PlayerPossibleState
{
    public GameObject PreFab;
    public CinemachineVirtualCameraBase Camera;
    public Vector3 StartPosition;

    public TypeOfPlayer PlayerType;
}

public class ManagerLevel : GenericSingleton<ManagerLevel>
{
    [SerializeField] private List<PlayerPossibleState> playerList;
    [SerializeField] private CinemachineBrain brainCamera;
    [SerializeField] private CinemachineVirtualCameraBase midCamera;

    [SerializeField] private GameObject islandMenu;
    [SerializeField] private GameObject playerUI;

    private GameObject currentLevel;

    private void Start() => SetUpForMainMenu();
    private void SetUpForMainMenu()
    {
        brainCamera.m_DefaultBlend.m_Time = 3f;

        if (Game_Manager.Instance != null) Game_Manager.Instance.EnableDisableClock(false);

        foreach (PlayerPossibleState player in playerList)
        {
            if (player.PlayerType != TypeOfPlayer.OnMenu) player.PreFab.SetActive(false);

            if (player.PlayerType == TypeOfPlayer.OnMenu) player.Camera.Priority = 20;
            else player.Camera.Priority = 1;

            player.PreFab.transform.position = player.StartPosition;
            if (player.PlayerType == TypeOfPlayer.OnPlay) player.PreFab.transform.rotation = Quaternion.identity;
        }

        playerUI.SetActive(false);
        islandMenu.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        if (NewManagerMenu.Instance == null || NewManagerMenu.Instance.CanReturnToMainMenu()) return;


        Debug.Log("TryMenu");
        StartCoroutine(ReturnToMainMenuRoutine());
    }

    private IEnumerator ReturnToMainMenuRoutine()
    {
        brainCamera.m_DefaultBlend.m_Time = 2f;

        midCamera.Priority = 20;

        PlayerPossibleState menuPlayer = playerList.Find(p => p.PlayerType == TypeOfPlayer.OnMenu);
        menuPlayer.Camera.Priority = 1;

        PlayerPossibleState playPlayer = playerList.Find(p => p.PlayerType == TypeOfPlayer.OnPlay);
        playPlayer.Camera.Priority = 1;

        yield return new WaitForSeconds(2);

        menuPlayer.Camera.Priority = 20;
        midCamera.Priority = 1;
        SetUpForMainMenu();

        yield return new WaitForSeconds(1);

        NewManagerMenu.Instance.OnReturnToMainMenu();
    }

    public void OnPlay(GameObject level)
    {
        currentLevel = level;
        StartCoroutine(OnPlayRoutine(level,2));

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReTryPlay()
    {
        if (Game_Manager.Instance != null) Game_Manager.Instance.Status();
        foreach (PlayerPossibleState player in playerList)
        {
            if (player.PlayerType != TypeOfPlayer.OnFall) player.PreFab.SetActive(false);

            if (player.PlayerType == TypeOfPlayer.OnFall) player.Camera.Priority = 20;
            else player.Camera.Priority = 1;

            player.PreFab.transform.position = player.StartPosition;
            if (player.PlayerType == TypeOfPlayer.OnPlay) player.PreFab.transform.rotation = Quaternion.identity;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(OnPlayRoutine(currentLevel,3));
    }


    private IEnumerator OnPlayRoutine(GameObject level,float blendTimeStart)
    {
        if (Game_Manager.Instance != null) Game_Manager.Instance.EnableDisableClock(false);

        level.SetActive(true);
        brainCamera.m_DefaultBlend.m_Time = blendTimeStart;

        PlayerPossibleState fallPlayer = playerList.Find(p => p.PlayerType == TypeOfPlayer.OnFall);

        fallPlayer.PreFab.SetActive(true);
        fallPlayer.Camera.Priority = 20;

        PlayerPossibleState menuPlayer = playerList.Find(p => p.PlayerType == TypeOfPlayer.OnMenu);
        menuPlayer.Camera.Priority = 1;

        yield return new WaitForSeconds(1f);

        Transform fallGuy = fallPlayer.PreFab.transform;
        Vector3 currentPositionFallPlayer = fallGuy.position;
        float progress = 0f;

        while (progress < 1)
        {
            progress += Time.deltaTime * 0.25f;
            fallGuy.position = Vector3.Lerp(currentPositionFallPlayer, new Vector3(fallGuy.position.x, 12.5f, fallGuy.position.z), progress);

            yield return null;
        }
        Debug.Log("End Fall");

        PlayerPossibleState playPlayer = playerList.Find(p => p.PlayerType == TypeOfPlayer.OnPlay);

        brainCamera.m_DefaultBlend.m_Time = 1f;
        playPlayer.Camera.Priority = 20;
        fallPlayer.Camera.Priority= 1;

        islandMenu.SetActive(false);
        fallPlayer.PreFab.SetActive(false);

        playPlayer.PreFab.SetActive(true);
        playerUI.SetActive(true);

        if (NewManagerMenu.Instance != null) NewManagerMenu.Instance.IsOnTheLevel();
        if (Game_Manager.Instance != null)
        {
            Game_Manager.Instance.Status();
            Game_Manager.Instance.EnableDisableClock(true);
        }
    }
}
