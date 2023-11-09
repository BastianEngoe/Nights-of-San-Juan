using HH.MultiSceneTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenusManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, settingsMenu;
    [SerializeField] private Toggle toggleScreen;
    [SerializeField] private Slider soundSlider, musicSlider;
    [HideInInspector] public bool onMenu = true;

    private void Start()
    {
        toggleScreen.onValueChanged.AddListener(ToggleFullScreen);
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
        onMenu = false;
    }

    public void NewGame()
    {
        MultiSceneLoader.loadCollection("Luarca", collectionLoadMode.Difference);
        mainMenu.SetActive(false);
        GameManager.instance.ActivatePlayer();
        Cursor.visible = false;
        onMenu = false;
    }

    public void ToMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }


    public void ToSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void ToggleFullScreen(bool toggleFullScreen)
    {
        Screen.fullScreen = toggleFullScreen;
    }

    public void ChangeSoundVolume()
    {
        SoundManager.instance.ChangeSoundsVolume(soundSlider.value);
    }

    public void ChangeMusicVolume() 
    {
        SoundManager.instance.ChangeMusicVolume(musicSlider.value);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
