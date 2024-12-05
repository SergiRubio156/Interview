using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Importa el namespace System.Linq para usar la función Any()

public class EvaluateManager : MonoBehaviour
{
    //RobotEspeficicaciones
    public List<RandomImageGenerator> RobotCards;
    public static int totalId = 0;

    ObjectManager objectManager;

    //Robot que le llega
    float numUp, numDown;
    float robotParts;
    int idRobot;
    Color colorRobot;
    int minutesRobot;

    public TimeManager timeManager;
    private void OnEnable()
    {
        objectManager = FindObjectOfType<ObjectManager>();

    }
    public void RecivedRobotsCards()
    {
        RandomImageGenerator[] components = FindObjectsOfType<RandomImageGenerator>();

        for (int i = 0; i < components.Length; i++)
        {
            // Verificar si ya existe un componente con el mismo nombre en el array RobotCards
            if (!RobotCards.Any(card => card.name == components[i].name))
            {
                totalId++;
                RobotCards.Add(components[i]);
                RobotCards[totalId - 1].SetIdCard(totalId - 1);
            }
        }
    }

    void FindId()
    {
        foreach (RandomImageGenerator component in RobotCards)
        {
            if (idRobot == component.GetIdCard())
            {
                if(CompareNumUp(component.numUp) && CompareNumDown(component.numDown) && RobotParts(component.RobotId()) && ColorRobot(component.ColorId()) && MinutesRobot(component.numberClock))
                {
                    timeManager.numTrueVictoryRobot++;
                }
            }
        }
    }

    bool MinutesRobot(int _num)
    {
        if (minutesRobot == _num)
        {
            return true;
        }
        return false;
    }

    bool RobotParts(float _num)
    {
        if (robotParts == _num)
        {
            return true;
        }
        return false;
    }
    bool ColorRobot(Color _color)
    {
        if (colorRobot == _color)
        {
            return true;
        }
        return false;
    }
    bool CompareNumUp(float _num)
    {
        if(numUp == _num)
        {
            return true;
        }
        return false;
    }
    bool CompareNumDown(float _num)
    {
        if (numDown == _num)
        {
            return true;
        }
        return false;
    }


    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.CompareTag("Interactable"))
        {
            Objects _obj = obj.gameObject.GetComponent<Objects>();
            idRobot = _obj.id;
            numUp = _obj.numUp;
            numDown = _obj.numDown;
            float robotUp = _obj.robotUp;
            float robotDown = _obj.robotDown;
            minutesRobot = _obj.timeColor;
            colorRobot = _obj.FinalColor;
            robotParts = robotUp + robotDown;
            FindId();
            RandomImageGenerator.instance.GenerateNewRobot();
            totalId++;
            timeManager.totalRobots++;
            timeManager.durationBetweenRobots = false;
            objectManager.RemoveRobotsList(_obj);
            Destroy(obj.gameObject);
        }

    }

}
