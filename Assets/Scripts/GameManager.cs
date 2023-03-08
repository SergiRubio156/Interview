using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

//Vamos a crear un Script Manager donde podremos coordinar todos los script 


//Vamos a crear un enum, un enum es como una lista de constantes enteras con nombres que nos sirven
//para poder pasar de fases del juego, podriamos decir que pasar de opciones a jugar es 
//estado del juego
public enum GameState
{
    Playing,
    Lasers,
    Settings,
    Menu


};

public class GameManager : MonoBehaviour
{
    public static GameManager _instance; //Creamos una variable en Static para que todos los otros scripts puedan utilizar este script

    public GameObject[] managers =  new GameObject[2];

    public static GameObject instance;

    //un evento static esta disponible para todos los scripts en cualquier momento, incluso si no existe ninguna instancia de la clase
    //Nosotros utilizaremos el evento para decir que si estas en GameState.menu el objeto PanelMenu se active, un evento te permite comparar scripts con Unity.GameObjects
    public static event Action<GameState> OnGameStateChanged; 

    GameState State;

    private int NumScene = 0;




    //Una funcion statica, es una funcion que nos permite llamar al codigo sin necesidad de una instancia, en palabras pa tontos
    //El script GameManager con todas sus variables y funciones (que aparezcan en public) apareceran en todos los scripts, eso nos
    //permitira llamarlos quando sea necesario.
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager is Null!!!!"); //Si sale esto es que el GameManager no funciona
            return _instance;
        }


    }

    void Awake() //El awake se entra igualmente que el script esta desactivado
    {
        _instance = this;        

    }

    void Start() //Solo se entra una vez, pero si el script esta desactivado no entra
    {
        UpdateGameState(GameState.Menu);//Entro en la funcion UpdateGameState, y ponemos como referencia el GameState.Menu porque es el stado que queremos
        managers = GameObject.FindGameObjectsWithTag("GameManager");
        DontDestroyOnLoad(this.gameObject); //Esto lo que hace es que no se destruya lobjeto quando se cambia de escena
        if (managers[1] != null)
           Destroy(managers[1].gameObject); //!PREGUNTAR PROFESOR, PORK APARECEN GAMEMANAGER CADA VEZ QUE SE ENTRA A UNA SCENA NUEVA, Y KIERO ELIMINARLO, HE PODIDO ELIMINARLO PERO ME PONE GAMEMANAGER NULL PERO AUN ASI SIGUE FUNCIONANDO
    }

    // Update is called once per frame
    void Update(){}

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        //Switch es com una argupacion de IFs uitilizando una variable comun, en este caso cogemos los valores de GameState(Playing,Lasers,Settings,Menu)
        //y le decimos que dependiendo del valor de "newState" entrara al valor correspondiente, por ejemplo, si "newState" = a GameState.Menu, entrara al Menu

        switch (newState)
        {
            case GameState.Menu:
                HandleSelectButton();
                break;//break se utiliza para romper el "IF"
            case GameState.Playing:
                break;
            case GameState.Lasers:
                HandlePlayerLasers();
                break;
            case GameState.Settings:
                HandleSettings();
                break;
            default: //se entrara aqui si el valor "newState" no coincide con ningun valor anterior
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);//pone el valor "newState" a null para que no pete el programa.
        }
        OnGameStateChanged?.Invoke(newState);//Esta linia se utiliza en que si algun otro script se cambia el estado del juego (si pasa de Menu a Playing) se invocara este linia 
        //y volvera a entrar al switch y se cambiara de estado.

        Debug.Log(newState);
    }

    private void HandlePlayerLasers()
    {
        if (NumScene != 1)
        {
            NumScene = 1;
            SceneManager.LoadScene("Lasers");

        }
    }

    private void HandleSelectButton()
    {
        Cursor.visible = true;
        if (NumScene != 0)
        {
            NumScene = 0;
            SceneManager.LoadScene("Menu");
        }
    }

    private void HandleSettings()
    {
        
    }


}