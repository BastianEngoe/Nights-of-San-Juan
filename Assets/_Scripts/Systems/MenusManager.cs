using HH.MultiSceneTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenusManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, settingsMenu,pauseMenu;
    [SerializeField] private Toggle toggleScreen;
    [SerializeField] private JournalManager journalManager;
    [SerializeField] private CameraManager camManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Slider soundSlider, musicSlider;
    [HideInInspector] public bool onMenu = true;

    private void Start()
    {
        toggleScreen.onValueChanged.AddListener(ToggleFullScreen);
        Cursor.visible = true;
        GameManager.instance.DeactivatePlayer();
        Cursor.lockState = CursorLockMode.Confined;
        SetSettingsFromPlayerPrefs();
    }

    private void SetSettingsFromPlayerPrefs()
    {
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        SoundManager.instance.ChangeMusicVolume(musicSlider.value);
        SoundManager.instance.ChangeSoundsVolume(soundSlider.value);
        ToggleFullScreen(Convert.ToBoolean(PlayerPrefs.GetInt("FullScreen", 0)));
        toggleScreen.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("FullScreen", 0));
    }

    public void ContinueGame()
    {
        MultiSceneLoader.loadCollection("Luarca", collectionLoadMode.Difference);
        mainMenu.SetActive(false);
        Cursor.visible = false;
        GameManager.instance.ActivatePlayer();  
        onMenu = false;
        WorldSaveSystem.instance.LoadAll();
    }

    public void NewGame()
    {
        MultiSceneLoader.loadCollection("Luarca", collectionLoadMode.Difference);
        mainMenu.SetActive(false);
        GameManager.instance.ActivatePlayer();
        Cursor.visible = false;
        onMenu = false;
    }

    public void ToOriginMenu()
    {
        if (!inputManager.onPause)
            mainMenu.SetActive(true);
        else
            pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void ToSettings()
    {
        if(!inputManager.onPause)
        mainMenu.SetActive(false);
        else
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void ToMainMenu()
    {
        MultiSceneLoader.loadCollection("Boot", collectionLoadMode.Difference);
        Time.timeScale = 1.0f;
        camManager.UpdateCameraState(CameraState.MoveState);
        inputManager.onPause = false;
        pauseMenu.SetActive(false);
        mainMenu.SetActive(true);
        GameManager.instance.DeactivatePlayer();
        GameManager.instance.onPause = false;
        onMenu = true;
    }

    public void ToggleFullScreen(bool toggleFullScreen)
    {
        PlayerPrefs.SetInt("FullScreen", toggleFullScreen? 1:0);
        Screen.fullScreen = toggleFullScreen;
    }

    public void SaveGame()
    {
        journalManager.UpdateJournal();
    }

    public void ChangeSoundVolume()
    {
        SoundManager.instance.ChangeSoundsVolume(soundSlider.value);
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
    }

    public void ChangeMusicVolume() 
    {
        SoundManager.instance.ChangeMusicVolume(musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
