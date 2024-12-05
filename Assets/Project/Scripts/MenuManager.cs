using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject settingsMenuPlay;
    public GameObject settingsMenu;
    public GameObject settingsMenuLasers;
    public GameObject tutorialLasers;
    public GameObject tutorialTaller;
    public GameObject panelVictory;
    public GameObject toppingPanel;
    public GameObject comandaRobot;
    public GameObject robotPanel;
    public GameObject panelColor;
    public GameObject armPanel;
    public TimeManager timeManager;
    public GameObject exam;

    public GameObject WireMenu;

    private GameState state;
    public bool sceneSettings = false;
    bool boolRobotPanel = true;
    public int numPulsarM;


    private void Awake()
    {
        state = GameManager.Instance.State;
    }
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState newState)    //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {
        switch (newState)
        {
            case GameState.Playing:
                sceneSettings = false;

                if (state != GameState.Menu)
                {
                    if (settingsMenuLasers.activeSelf)
                        settingsMenuLasers.SetActive(false);
                    tutorialLasers.SetActive(false);
                    settingsMenuPlay.SetActive(false);
                    panelVictory.SetActive(false);
                    toppingPanel.SetActive(false);
                    WireMenu.SetActive(false);
                    panelColor.SetActive(false);
                    armPanel.SetActive(false);
                    robotPanel.SetActive(false);
                }
                state = newState;
                break;
            case GameState.Lasers:
                sceneSettings = false;
                tutorialLasers.SetActive(true);
                settingsMenuLasers.SetActive(false);
                state = newState;
                break;
            case GameState.Settings:
                if(comandaRobot != null)
                    comandaRobot.SetActive(false);
                if (state == GameState.Playing)
                    settingsMenuPlay.SetActive(true);
                else if (state == GameState.Lasers)
                {
                    settingsMenuLasers.SetActive(true);
                }
                else if(state == GameState.Menu)
                {
                    settingsMenu.SetActive(true);
                }
                sceneSettings = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Menu:
                settingsMenu.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                state = newState;
                break;
            case GameState.Wire:
                comandaRobot.SetActive(false);
                WireMenu.SetActive(true);
                sceneSettings = false;
                state = newState;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Topping:
                comandaRobot.SetActive(false);
                toppingPanel.SetActive(true);
                sceneSettings = false;
                state = newState;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Color:
                panelColor.SetActive(true);
                comandaRobot.SetActive(false);
                sceneSettings = false;
                state = newState;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.RobotPanel:
                robotPanel.SetActive(true);
                comandaRobot.SetActive(false);
                sceneSettings = false;
                state = newState;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.ArmPanel:
                armPanel.SetActive(true);
                comandaRobot.SetActive(false);
                sceneSettings = false;
                state = newState;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Exam:
                exam.SetActive(true);
                sceneSettings = true;
                state = newState;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Exit:
                // Acciones a realizar cuando el estado de juego es "Exit"
                break;
        }
    }

    private void Update()
    {
        switch(state)
        {
            case GameState.Menu:
                break;
            case GameState.Exam:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                   GameManager.Instance.State = GameState.Menu;
                }
                break;
            case GameState.Playing:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (sceneSettings) GameManager.Instance.State = GameState.Playing;
                    else GameManager.Instance.State = GameState.Settings;
                }
                if (Input.GetKeyDown(KeyCode.P))
                {
                    if (boolRobotPanel) { comandaRobot.SetActive(true); boolRobotPanel = false; timeManager.totalHelpLvl2++; }
                    else { comandaRobot.SetActive(false); boolRobotPanel = true; }
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    GameManager.Instance.State = GameState.Exam;
                }
                break;
            case GameState.Lasers:
                if (Input.GetKeyDown(KeyCode.Escape)) 
                {
                    if (sceneSettings) GameManager.Instance.State = GameState.Lasers;
                    else GameManager.Instance.State = GameState.Settings;
                }
                break;
            case GameState.Wire:
                if (Input.GetKeyDown(KeyCode.Escape)) 
                {
                    if (sceneSettings) GameManager.Instance.State = GameState.Wire;
                    else GameManager.Instance.State = GameState.Playing;
                }
                break;
            case GameState.Topping:
                if (Input.GetKeyDown(KeyCode.Escape)) 
                {
                    if (sceneSettings) GameManager.Instance.State = GameState.Topping;
                    else GameManager.Instance.State = GameState.Playing;
                }
                break;
            case GameState.Color:
                if (Input.GetKeyDown(KeyCode.Escape)) 
                {
                    if (sceneSettings) GameManager.Instance.State = GameState.Color;
                    else GameManager.Instance.State = GameState.Playing;
                }
                break;
            case GameState.RobotPanel:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (sceneSettings) GameManager.Instance.State = GameState.RobotPanel;
                    else GameManager.Instance.State = GameState.Playing;
                }
                break;
        }
    }
    public void StartGame() //Esta funcion se llama cuando le damos click al botton del Menu
    {
        GameManager.Instance.State = GameState.Playing;
    }

    public void StartSettings() //Esta funcion se llama cuando le damos click al botton del Menu
    {
        GameManager.Instance.State = GameState.Settings;
    }
    public void StartMenu() //Esta funcion se llama cuando le damos click al botton del Menu
    {
        GameManager.Instance.State = GameState.Menu;
    }
    public void ExitToGame() 
    {
        GameManager.Instance.State = GameState.Exit;
    }
    public void ExitToPlay()
    {
        timeManager.vecesSalirLvl1++;
        GameManager.Instance.State = GameState.Playing;
    }

}                                                                                                                                                                                                                                                                                    
