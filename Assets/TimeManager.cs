using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool durationLvl1;
    [SerializeField]
    float timeDuration1;

    public bool durationLvl2;
    [SerializeField]
    float timeDuration2;

    //BETWEEEN ROBOTS
    public bool durationBetweenRobots;
    [SerializeField]
    List<float> timeDurationBetweenRobots;
    public float totalDurationRobots;
    public float numTrueVictoryRobot;
    public float totalRobots;

    //BETWEEN LASERS
    public bool durationBetweenLasers;
    [SerializeField]
    List<float> timeDurationBetweenLasers;
    public float totalDurationLasers;
    public int totalLvl = -1;
    public int totalHelpLvl1;
    public int totalHelpLvl2;
    public int vecesSalirLvl1;
    public int numTrueVictory;

    float speed = 1f;
    public bool pos; 
    public bool posLasers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(durationLvl1)
        {
            timeDuration1 += Time.deltaTime * speed;
        }

        if(durationLvl2)
        {
            timeDuration2 += Time.deltaTime * speed;
        }

        if (durationBetweenRobots)
        {
            timeDurationBetweenRobots[EvaluateManager.totalId - 1] += Time.deltaTime * speed;
            pos = true;
        }
        else if(pos)
        {
            totalDurationRobots += timeDurationBetweenRobots[EvaluateManager.totalId - 2];
            pos = false;
        }

        if (durationBetweenLasers)
        {
            timeDurationBetweenLasers[totalLvl] += Time.deltaTime * speed;
            posLasers = true;
        }
        else if (posLasers)
        {
            totalDurationLasers += timeDurationBetweenLasers[totalLvl];
            posLasers = false;
        }
    }

    public float time(string _name)
    {
        switch(_name)
        {
            case "Lvl1":
                return timeDuration1;
            case "Lvl2":
                return timeDuration2;
            case "betweenRobots":
                return totalDurationRobots / totalRobots;            
            case "betweenLasers":
                float _int2 = totalLvl + 1;
                return totalDurationLasers / _int2;
            case "denominador":
                float _int = totalLvl + 1;
                return (numTrueVictory / _int) * 100;
            case "denominadorRobot":
                Debug.Log(numTrueVictoryRobot);
                Debug.Log(totalRobots);
                return (numTrueVictoryRobot / totalRobots) * 100;
        }
        return 0.0f;
    }
}
