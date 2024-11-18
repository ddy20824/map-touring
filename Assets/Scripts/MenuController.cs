using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject[] buttons;
    GameObject panel;
    int select_Button = 0;
    bool menuOpen;
    // Start is called before the first frame update
    void Start()
    {
        buttons[select_Button].GetComponent<Button>().Select();
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
        if (Input.GetKeyDown(KeyCode.UpArrow) && menuOpen)
        {
            if (select_Button > 0)
            {
                select_Button--;
                ManageButton();
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && menuOpen)
        {
            if (select_Button < buttons.Length - 1)
            {
                select_Button++;
                ManageButton();
            }

        }
    }
    void OpenMenu()
    {
        GameState.Instance.SetPlayerFronze(true);
        panel.SetActive(true);
        menuOpen = true;
        buttons[0].GetComponent<Button>().Select();
    }

    void CloseMenu()
    {
        GameState.Instance.SetPlayerFronze(false);
        panel.SetActive(false);
        menuOpen = false;
    }

    void ManageButton()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == select_Button)
            {
                buttons[i].GetComponent<Button>().Select();
            }
        }
    }
    public void NewGame()
    {
        Debug.Log("New Game");
        DataPersistentManager.instance.NewGame();
        // GameState.Instance.InitGame();
        // dataPersistence.LoadGame();
        CloseMenu();
    }

    public void LoadGame()
    {
        Debug.Log("Load Game");
        DataPersistentManager.instance.LoadGame();
        // GameState.Instance.LoadGame("Save.sav");
        // dataPersistence.LoadGame();
        CloseMenu();
    }

    public void SaveGame()
    {
        Debug.Log("Save Game");
        DataPersistentManager.instance.SaveGame();
        // dataPersistence.SaveGame();
        // GameState.Instance.SaveGame("Save.sav");
        CloseMenu();
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
