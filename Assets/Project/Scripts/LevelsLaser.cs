using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsLaser : MonoBehaviour
{
    public string name = "Level1";
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


    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.State = GameState.Lasers;

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.1f);
        GameManager.Instance.State = GameState.Lasers;

    }

}
