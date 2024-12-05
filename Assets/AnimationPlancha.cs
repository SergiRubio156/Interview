using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlancha : MonoBehaviour
{
    public Animator animator;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
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
                animator.SetBool("Plancha", false);
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

                break;
            case GameState.RobotPanel:
                animator.SetBool("Plancha", true);
                break;
        }
    }

}
