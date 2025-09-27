using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    public enum PanelType { menu,option,levels}

    [Header("Setting Pannels")]
    [SerializeField] private GameObject[] panels;

    [Header("Setting Name Levels")]
    [SerializeField] private string LevelOne = "Level1";
    [SerializeField] private string LevelTwo = "Level2";

    private void Start() => SetPanel(PanelType.menu);

    private void SetPanel(PanelType panelType)
    {
        for (int i = 0; i < panels.Length; i++) panels[i].SetActive(i == (int)panelType);
    }

    public void GoToMenu() => SetPanel(PanelType.menu);

    public void GoToOption() => SetPanel(PanelType.option);

    public void GoToLevel() => SetPanel(PanelType.levels);

    public void GoLevelOne() => SceneManager.LoadScene(LevelOne);

    public void GoLevelTwo() => SceneManager.LoadScene(LevelTwo);

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
