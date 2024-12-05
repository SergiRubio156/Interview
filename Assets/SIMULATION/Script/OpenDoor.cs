using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    private TransitionCamera transitionCamera;

    public bool lvlComplete;
     bool isOpen;


    [SerializeField]
    Animator doorAnimator;
    public AudioSource audioSource;
    public AudioClip audioClip;
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
                if (!isOpen)
                {
                    lvlComplete = transitionCamera.lvlComplete;
                    if (lvlComplete)
                    {
                        isOpen = true;
                        audioSource.enabled = true;
                        audioSource.Play();
                        doorAnimator.SetBool("OpenDoor", true);
                    }
                }
                break;
            case GameState.Lasers:
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
        transitionCamera = GetComponentInChildren<TransitionCamera>();
        doorAnimator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (lvlComplete)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                audioSource.Play();
                doorAnimator.SetBool("OpenDoor", true);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (lvlComplete)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                audioSource.Play();
                doorAnimator.SetBool("OpenDoor", false);
            }
        }
    }
}
