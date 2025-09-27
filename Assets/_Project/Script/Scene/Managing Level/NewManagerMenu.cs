using System.Collections;
using UnityEngine;

public class NewManagerMenu : GenericSingleton<NewManagerMenu>
{
    [SerializeField] private GameObject pannelMenu;

    [SerializeField] private Animator animatorPlayerMenu;

    [SerializeField] private GameObject level;
    [SerializeField] private GameObject levelTwo;

    private bool isGoOnTheLevel;
    private bool isOnPlayMode;

    private void Update()
    {
        Debug.Log(isOnPlayMode);
    }
    public void OnPlay()
    {
        Debug.Log("TryTo GoLevel 1");
        if (isGoOnTheLevel || isOnPlayMode) return;

        isOnPlayMode = true;
        isGoOnTheLevel = true;
        
        Debug.Log("GoLevel 1");
        if (ManagerLevel.Instance != null) ManagerLevel.Instance.OnPlay(level);
        animatorPlayerMenu.SetTrigger("Fall");
    }

    public void OnPlayTwo()
    {
        Debug.Log("TryTo GoLevel 2");
        if (isGoOnTheLevel || isOnPlayMode) return;

        isOnPlayMode = true;
        isGoOnTheLevel = true;

        Debug.Log("GoLevel 2");
        if (ManagerLevel.Instance != null) ManagerLevel.Instance.OnPlay(levelTwo);
        animatorPlayerMenu.SetTrigger("Fall");
    }

    public bool CanReturnToMainMenu() => isGoOnTheLevel;
    public void IsOnTheLevel() => isGoOnTheLevel = false;
    public void OnReturnToMainMenu() => StartCoroutine(UnAliveLevelsRoutine());

    private IEnumerator UnAliveLevelsRoutine()
    {
        Debug.Log("ReturnMenuNew");
        isGoOnTheLevel = true;
        yield return new WaitForSeconds(2);

        level.SetActive(false);
        levelTwo.SetActive(false);

        isOnPlayMode = false;
        isGoOnTheLevel = false;
    }
}
