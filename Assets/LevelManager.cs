using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> levelPrefabs;

    public TimeManager timeManager;

    bool isPlaying;

    bool oneTime;
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.Playing:
                if (isPlaying)
                    RemoveLevel(GetNameLevel());
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
                break;
            case GameState.ArmPanel:

                break;
        }
    }

    public void ChooseLevel()
    {
        GameObject objectHijo = Instantiate(levelPrefabs[0], gameObject.transform, true);

        if(!oneTime)
        {
            timeManager.durationLvl1 = true;

            oneTime = true;
        }
        timeManager.totalLvl++;
        timeManager.durationBetweenLasers = true;


        isPlaying = true;
    }

    public void RemoveLevel(string _name)
    {

        GameObject child = gameObject.transform.GetChild(0).gameObject;

        if (child != null)
        {
            Destroy(child);
        }

        isPlaying = false;
    }

    public void RemoveLvlList(string _name)
    {
        foreach (GameObject elemento in levelPrefabs)
        {
            if (elemento.name == _name)
            {
                levelPrefabs.RemoveAll(obj => obj.name == _name);
                break;
            }
        }

        RemoveLevel(_name);
    }

    public string GetNameLevel()
    {
        return levelPrefabs[0].name;
    }
}
