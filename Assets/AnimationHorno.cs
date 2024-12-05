using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHorno : MonoBehaviour
{
    public Animator animator;
    public Objects obj;
    ObjectManager objectManager;
    bool isPlaying, robotIn;
    public GameObject pos;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
        objectManager = FindObjectOfType<ObjectManager>();
        animator = gameObject.GetComponent<Animator>();
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
                break;
            case GameState.Lasers:

                break;
            case GameState.Settings:
                break;
            case GameState.Menu:

                break;
            case GameState.Wire:

                break;
            case GameState.Topping:

                break;
            case GameState.Color:
                isPlaying = true;
                obj = objectManager.FindStateOfObject(ObjectState.Color);
                robotIn = true;
                pos.SetActive(false);
                break;
            case GameState.RobotPanel:
                break;
        }
    }

    private void Update()
    {
        if (isPlaying)
        {
            if (obj.colorCheck)
            {
                robotIn = true;
                animator.SetBool("Horno", true);
            }
            else if(robotIn && obj.state != ObjectState.Color)
            {
                pos.SetActive(true);
                animator.SetBool("Horno", false);
                isPlaying = false;
                robotIn = false;
            }
            
        }
    }
}
