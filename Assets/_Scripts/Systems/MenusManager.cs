using HH.MultiSceneTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private bool onMenuChanged = true;
    [SerializeField] private GameObject continueButton;
    private Vector3 initialPlayerPos;

    private List<GameObject> currentMenuObjects = new List<GameObject>();
    private int currentMenuIndex=0;

    public static MenusManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        continueButton.SetActive(WorldSaveSystem.instance.CheckSave());
        toggleScreen.onValueChanged.AddListener(ToggleFullScreen);
        Cursor.visible = true;
        GameManager.instance.DeactivatePlayer();
        Cursor.lockState = CursorLockMode.Confined;
        SetSettingsFromPlayerPrefs();
        initialPlayerPos = GameManager.instance.playerObject.transform.position;
    }

    private void Update()
    {
       if(onMenuChanged)
        {
            GameManager.instance.inputManager.SetMenuControls(onMenu);
            if (onMenu)
                GetObjectsOfMenu();
            onMenuChanged= false;
        }
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

    public void GetObjectsOfMenu()
    {
        currentMenuObjects.Clear();
        currentMenuIndex = 0;
        for(int i=0; i<gameObject.transform.childCount; i++)
        {
            Transform currentObject = gameObject.transform.GetChild(i);
            if (currentObject.tag == "Menu" && currentObject.gameObject.activeSelf)
            {
                for(int j=0; j<currentObject.childCount; j++)
                {
                    if(currentObject.GetChild(j).GetComponent<Button>()||currentObject.GetChild(j).GetComponent<Slider>())
                    currentMenuObjects.Add(currentObject.GetChild(j).gameObject);
                }
            }
        }
        EventSystem.current.SetSelectedGameObject(currentMenuObjects[currentMenuIndex]);

    }

    public void ContinueGame()
    {
        MultiSceneLoader.loadCollection("Luarca", collectionLoadMode.Difference);
        SoundManager.instance.PlayMusicOnRandomInterv();
        mainMenu.SetActive(false);
        Cursor.visible = false;
        GameManager.instance.ActivatePlayer();
        onMenuChanged = true;
        onMenu = false;
        WorldSaveSystem.instance.LoadAll();
    }

    public void NewGame()
    {
        MultiSceneLoader.loadCollection("Luarca", collectionLoadMode.Difference);
        mainMenu.SetActive(false);
        GameManager.instance.ActivatePlayer();
        GameManager.instance.playerObject.transform.position= initialPlayerPos;
        Cursor.visible = false;
        onMenuChanged = true;
        onMenu = false;
    }

    public void ToOriginMenu()
    {
        if (!inputManager.onPause)
        {
            mainMenu.SetActive(true);
            SoundManager.instance.StopMusic();
        }
        else
            pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        GetObjectsOfMenu();
    }
    

    public void ToSettings()
    {
        if(!inputManager.onPause)
        mainMenu.SetActive(false);
        else
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        GetObjectsOfMenu();
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
        onMenuChanged=true;
        GameManager.instance.onPause = false;
        onMenu = true;
        GetObjectsOfMenu();
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

    public void MenuMove(Vector2 moveValue)
    {
        if(!EventSystem.current.currentSelectedGameObject)
        {
            EventSystem.current.SetSelectedGameObject(currentMenuObjects[currentMenuIndex]);
        }
    }
}
