using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject panelMenu;
    public GameObject settingsMenu;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged; //Esto es el evento del script GameManager
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged; //La funcion "OnDestroy" se activa cuando destruimos el objeto, una vez destruido se activa el evento,
    }

    private void GameManager_OnGameStateChanged(GameState state)    //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {
        panelMenu.SetActive(state == GameState.Menu);   //Si el GameState es Menu se activa este panel

        settingsMenu.SetActive(state == GameState.Settings);        //Si el GameState es Settings se activa este panel

    }


    public void StartGame(GameObject panel) //Esta funcion se llama cuando le damos click al botton del Menu
    {
        panel.SetActive(false);//Se desactiva el panel

        GameManager.Instance.UpdateGameState(GameState.Lasers);//Utilizando la instancia del GameManager, entramos a la funcion UpdateGameState, y cambiamos el State a Lasers
    }
    public void SettingsGame(GameObject panel)//Esta funcion se llama cuando le damos click al botton del Settings
    {
        panel.SetActive(false);//Se desactiva el panel

        GameManager.Instance.UpdateGameState(GameState.Lasers);//Utilizando la instancia del GameManager, entramos a la funcion UpdateGameState, y cambiamos el State a Lasers

    }
}                                                                                                                                                                                                                                                                                    
