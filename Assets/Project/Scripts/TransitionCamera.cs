using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TransitionCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    public float transitionTimeDoor = 0.5f;

    public bool lvlComplete = false;

    [SerializeField]
    private string nameDoor;
  
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged; //Esto es el evento del script GameManager
        nameDoor = transform.parent.gameObject.name;
        CheckDoor();
    }
    private void GameManager_OnGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.Lasers:
                
                break;
            case GameState.Playing:
                CheckDoor();
                break;
            case GameState.Settings:
                
                break;
            case GameState.Menu:
                break;
            case GameState.Wire:
                break;
            case GameState.Exit:
                // Acciones a realizar cuando el estado de juego es "Exit"
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cam2 = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    void CheckDoor()
    {
        lvlComplete = GameManager.Instance.OpenDoor(nameDoor);
    }
    public void transitionScene(string _name)
    {
        if (!lvlComplete)
        {
            if (_name == "Door")
            {
                StartCoroutine(TransicionDeCamara());
            }
        }

    }
    private System.Collections.IEnumerator TransicionDeCamara()
    {
        // Activa la cámara 1
        cam2.enabled = true;

        yield return new WaitUntil(() => !CinemachineCore.Instance.IsLive(cam1));
        GameManager.Instance.nameDoors(nameDoor);
        CheckDoor();

        GameManager.Instance.State = GameState.Lasers;

        RevertTransitionScene();
    }


    public void RevertTransitionScene()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }

}