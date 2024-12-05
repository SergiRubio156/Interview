using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Vamos a crear un Script Manager donde podremos coordinar todos los script 


//Vamos a crear un enum, un enum es como una lista de constantes enteras con nombres que nos sirven
//para poder pasar de fases del juego, podriamos decir que pasar de opciones a jugar es 
//estado del juego
public enum GameState
{
    Playing,
    Lasers,
    Settings,
    Menu,
    Wire,
    Topping,
    Color,
    RobotPanel,
    ArmPanel,
    Exam,
    Exit


};
[Serializable]
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameObject _instance;

    [SerializeField]
    private sceneManager sceneManager;

    [SerializeField]
    private List<string> levelLaser = new List<string>() { "Nivel 1", "Nivel 2", "Nivel 3", "Nivel 4", "Nivel 5", "Nivel 6", "Nivel 7", "Nivel 8", "Nivel 9" };

    [SerializeField]
    private List<string> doorObjects = new List<string>();

    public GameManagerData gameManagerData;

    public static event Action<GameState> OnGameStateChanged;

    private GameState state;

    string nameDoor;

    public bool doorFind;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Si no hay instancia existente, buscarla en la escena
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    // Si no se encuentra en la escena, crear una nueva instancia
                    GameObject go = new GameObject("GameManager");
                    instance = go.AddComponent<GameManager>();
                }

                //DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    void Awake()
    {
        state = GameState.Playing;

        DontDestroyOnLoad(this.gameObject);

        _instance = this.gameObject;

        if (sceneManager == null)
            sceneManager = GameObject.FindObjectOfType<sceneManager>();

        GameManager[] gameManagers = FindObjectsOfType<GameManager>();
        if (gameManagers.Length > 1)
        {
            // Hay más de un objeto GameManager, eliminar los duplicados
            for (int i = 1; i < gameManagers.Length; i++)
            {
                Destroy(gameManagers[i].gameObject);
            }
        }

    }
    private void Start()
    {
        //doorObjects = gameManagerData.objectList;
        _instance = this.gameObject;
    }


    public GameState State
    {
        get { return state; }
        set
        {
            if (sceneManager == null)
                sceneManager = GameObject.FindObjectOfType<sceneManager>();

            if (state != value)
            {
                //Switch es com una argupacion de IFs uitilizando una variable comun, en este caso cogemos los valores de GameState(Playing,Lasers,Settings,Menu)
                //y le decimos que dependiendo del valor de "newState" entrara al valor correspondiente, por ejemplo, si "newState" = a GameState.Menu, entrara al Menu

                switch (value)
                {
                    case GameState.Menu:
                        HandleMenu();
                        state = value;
                        break;//break se utiliza para romper el "IF"

                    case GameState.Playing:
                        if(state == GameState.Menu)
                            HandlePlaying();
                        state = value;
                        break;

                    case GameState.Lasers:
                        if (state == GameState.Playing)
                        {
                            HandlePlayerLasers();
                        }
                        state = value;
                        break;

                    case GameState.Settings:
                        HandleSettings();
                        state = value;
                        break;

                    case GameState.Exit:
                        HandleExit();
                        break;

                    case GameState.Wire:
                        HandleSettings();
                        state = value;
                        break;
                    case GameState.Topping:
                        HandleSettings();
                        state = value;
                        break;
                    case GameState.Color:
                        HandleSettings();
                        state = value;
                        break;
                    case GameState.RobotPanel:
                        HandleSettings();
                        state = value;
                        break;
                    case GameState.ArmPanel:
                        HandleSettings();
                        state = value;
                        break;
                    case GameState.Exam:
                        HandleSettings();
                        state = value;
                        break;
                    default: //se entrara aqui si el valor "newState" no coincide con ningun valor anterior
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);//pone el valor "newState" a null para que no pete el programa.
                }

            }
            OnGameStateChanged?.Invoke(state);//Esta linia comprueba si el estado ha cambiado y si es true entonces va a todos los scripts y cambia el estado.
        }
    }

    private void HandleExit()
    {
        Application.Quit();
    }

    private void HandlePlayerLasers()//string _name)
    {
        sceneManager.ChangeScene("Laser");
    }

    private void HandleMenu()
    {
        sceneManager.ChangeScene("Menu");
    }

    private void HandleSettings()
    {

    }

    private void HandlePlaying()
    {
        sceneManager.ChangeScene("Play");
    }

    public void nameDoors(string _name )
    {
        nameDoor = _name;
    }

    void DoorFind()
    {

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("FinalDoor");

        foreach (GameObject obj in gameObjects)
        {
            doorObjects.Add(obj.name);
        }

        doorFind = true;
    }
    public void DoorDelete(string _name)
    {
        foreach (string elemento in doorObjects)
        {
            if (elemento == _name)
            {
                doorObjects.Remove(_name);
                break;
            }
        }
    }
    public void LvlCompleted()
    {
        string _name = sceneManager.GetLevelName();
        DoorDelete(nameDoor);
        foreach (string elemento in levelLaser)
        {
            if (elemento == _name)
            {
                levelLaser.Remove(_name);
                sceneManager.RemoveLevel(_name);

                break;
            }
        }
    }

    public bool OpenDoor(string _name)
    {
        if (!doorFind)
            DoorFind();

        foreach (string elemento in doorObjects)
        {
            if (elemento == _name)
            {
                return false;
            }
        }
        return true;
    }

}
