using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCheck : MonoBehaviour
{
    public int numFinalLaser = 0;
    public GameObject[] FinalLaser = new GameObject[0];
    public bool[] CheckBool;

    public GameObject panelVictory;
    private MenuManager menuManager;
    TimeManager timeManager;
    bool victoryBool;
    
    // Start is called before the first frame update
    void Awake()
    {
        FinalLaser = GameObject.FindGameObjectsWithTag("LaserFinal");
        CheckBool = new bool[FinalLaser.Length];
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;   //Esto es el evento del script GameManager
        menuManager = GameObject.FindObjectOfType<MenuManager>();
        timeManager = GameObject.FindObjectOfType<TimeManager>();
        panelVictory = menuManager.panelVictory;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            menuManager.numPulsarM++;
            Victory();
        }
    }
    void GameManager_OnGameStateChanged(GameState state)        //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {

    }

    public void CheckList(bool _bool, string _name)
    {
        for (int i = 0; i < FinalLaser.Length; i++)
        {
            if (FinalLaser[i].name == _name)
            {
                CheckBool[i] = _bool;
                if (CheckBools())
                {
                    if (!victoryBool)
                    {
                        timeManager.numTrueVictory++;
                        Victory();
                    }
                }

            }

        }
    }

    bool CheckBools()
    {
        for (int b = 0; b < CheckBool.Length; b++)
        {
            if (!CheckBool[b])
                return false;
        }
        return true;
    }

    void Victory()
    {
        timeManager.durationBetweenLasers = false;
        victoryBool = true;
        panelVictory.SetActive(true);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.LvlCompleted();
        GameManager.Instance.State = GameState.Playing;

    }
}