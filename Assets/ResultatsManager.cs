using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ResultatsManager : MonoBehaviour
{
    public TextMeshProUGUI robotsTotal;
    public TextMeshProUGUI durationLvL1;
    public TextMeshProUGUI durationLvL2;
    public TextMeshProUGUI durationBetweenRobots;
    public TextMeshProUGUI numPulsarM;
    public TextMeshProUGUI porcentajeLvl1;
    public TextMeshProUGUI porcentajeLvl2;
    public TextMeshProUGUI vecesSalirLvl1;
    public TextMeshProUGUI mediaLasers;
    public TextMeshProUGUI totalHelpLvl1;
    public TextMeshProUGUI totalHelpLvl2;


    public GameObject frustracio;
    public GameObject adaptacio;
    public GameObject decisio;
    public GameObject productivitat;

    public Button derecha;
    public Button izquierda;

    public TimeManager timeManager;
    public MenuManager menuManager;
    // Start is called before the first frame update
    void Start()
    {
        timeManager.durationLvl2 = false;
        timeManager.durationBetweenRobots = false;
        timeManager.pos = false;
        timeManager.durationBetweenLasers = false;
        timeManager.posLasers = false;


        robotsTotal.text = timeManager.totalRobots.ToString();
        durationLvL1.text = timeManager.time("Lvl1").ToString();
        durationLvL2.text = timeManager.time("Lvl2").ToString();
        durationBetweenRobots.text = timeManager.time("betweenRobots").ToString();
        mediaLasers.text = timeManager.time("betweenLasers").ToString();
        numPulsarM.text = menuManager.numPulsarM.ToString();
        totalHelpLvl1.text = timeManager.totalHelpLvl1.ToString();
        totalHelpLvl2.text = timeManager.totalHelpLvl2.ToString();
        porcentajeLvl1.text = timeManager.time("denominador").ToString() + "%";
        porcentajeLvl2.text = timeManager.time("denominadorRobot").ToString() + "%";

        derecha.onClick.AddListener(Derecha);
        izquierda.onClick.AddListener(Izquierda);
    }

    void Izquierda()
    {
        if (decisio.activeSelf)
        {
            decisio.SetActive(false);
            adaptacio.SetActive(true);
        }
        else if (adaptacio.activeSelf)
        {
            adaptacio.SetActive(false);
            frustracio.SetActive(true);
        }
        else if (frustracio.activeSelf)
        {
            frustracio.SetActive(false);
            productivitat.SetActive(true);
        }
    }

    void Derecha()
    {
        if(productivitat.activeSelf)
        {
            productivitat.SetActive(false);
            frustracio.SetActive(true);
        }
        else if(frustracio.activeSelf)
        {
            frustracio.SetActive(false);
            adaptacio.SetActive(true);
        }
        else if(adaptacio.activeSelf)
        {
            adaptacio.SetActive(false);
            decisio.SetActive(true);
        }
    }
}
