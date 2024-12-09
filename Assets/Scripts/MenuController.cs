using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    public GameObject startButton;
    GameObject panel;
    bool menuOpen;
    // Start is called before the first frame update
    void Start()
    {
        startButton.GetComponent<Button>().Select();
        panel = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuOpen)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }
    void OpenMenu()
    {

        startButton.GetComponent<Button>().Select();
        GameState.Instance.SetPlayerFronze(true);
        panel.SetActive(true);
        menuOpen = true;
    }

    void CloseMenu()
    {
        GameState.Instance.SetPlayerFronze(false);
        panel.SetActive(false);
        menuOpen = false;
    }
    public void NewGame()
    {
        Debug.Log("New Game");
        DataPersistentManager.instance.NewGame();
        CloseMenu();
    }

    public void LoadGame()
    {
        Debug.Log("Load Game");
        DataPersistentManager.instance.LoadGame();
        CloseMenu();
    }

    public void SaveGame()
    {
        Debug.Log("Save Game");
        DataPersistentManager.instance.SaveGame();
        CloseMenu();
    }

    public void DisplayObject(GameObject openObject)
    {
        openObject.SetActive(true);
    }

    public void HiddenObject(GameObject closeObject)
    {
        closeObject.SetActive(false);
    }

    public void SetDefaultButton(GameObject defaultButton)
    {
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
