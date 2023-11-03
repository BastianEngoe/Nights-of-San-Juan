using HH.MultiSceneTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, settingsMenu;

    private void Start()
    {
        Cursor.visible = true;
        GameManager.instance.DeactivatePlayer();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ContinueGame()
    {
        MultiSceneLoader.loadCollection("Luarca", collectionLoadMode.Difference);
        mainMenu.SetActive(false);
        Cursor.visible = false;
        GameManager.instance.ActivatePlayer();
    }

    public void NewGame()
    {
        MultiSceneLoader.loadCollection("Luarca", collectionLoadMode.Difference);
        mainMenu.SetActive(false);
        GameManager.instance.ActivatePlayer();
        Cursor.visible = false;
    }

    public void ToSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
