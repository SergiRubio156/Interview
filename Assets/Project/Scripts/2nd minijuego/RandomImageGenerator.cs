using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class RandomImageGenerator : MonoBehaviour
{
    public static RandomImageGenerator instance;

    public List<Texture2D> images; // Array que contiene las imágenes que quieres mostrar
    public RawImage rawImage; // Referencia al componente RawImage para mostrar las imágenes
    public Texture2D[] pinturas; // Array que contiene las pinturas que quieres mostrar
    public RawImage pinturaImage; // Referencia al componente RawImage para mostrar las pinturas
    public List<TextMeshProUGUI> cpuText;
    public TextMeshProUGUI countText; // Referencia al componente de TextMeshProUGUI para el contador
    public EvaluateManager evaluateManager;

    //pasarAl evaluateManager

    [SerializeField]
    private int idCard;
    public float numDown, numUp;
    float robotUp, robotDown;
    int randomIndexRobot;
    int randomIndexColor;
    public int numberClock;
    private void Awake()
    {
        evaluateManager.RecivedRobotsCards();
        if (instance== null )
        {
            instance= this;
        }
    }
    private void Start()
    {
        GenerateNewRobot();
    }

    public int GetIdCard()
    {
        return idCard;
    }
    public void SetIdCard(int _num)
    {
        idCard = _num;
    }
    public void GenerateNewRobot()
    {
        IncrementCount();
        GenerateRandomImage();
        GenerateRandomPintura();
        GenerateRandomNumberCPU1();
        GenerateRandomNumberCPU2();
    }


    public void GenerateRandomImage()
    {
        randomIndexRobot = Random.Range(0, images.Count);
        rawImage.texture = images[randomIndexRobot];
    }

    public void GenerateRandomPintura()
    {
        randomIndexColor = Random.Range(0, pinturas.Length);
        pinturaImage.texture = pinturas[randomIndexColor];
    }


    public void IncrementCount()
    {
        idCard = EvaluateManager.totalId;
        countText.text = idCard.ToString();
    }

    public void GenerateRandomNumberCPU1()
    {
        float randomNumber = Random.Range(0f, 2f);
        randomNumber = Mathf.Round(randomNumber * 2f) / 2f; // Redondea al número más cercano con incremento de 0.5 o 1
        numUp = randomNumber;
        cpuText[0].SetText(randomNumber.ToString());
        
    }
    public void GenerateRandomNumberCPU2()
    {
        float randomNumber = Random.Range(0f, 2f);
        randomNumber = Mathf.Round(randomNumber * 2f) / 2f; // Redondea al número más cercano con incremento de 0.5 o 1
        numDown = randomNumber;
        cpuText[1].SetText(randomNumber.ToString());
    }

    public float RobotId()
    {
        switch(randomIndexRobot)
        {
            case 0:
                robotUp = 1;
                robotDown = 0.1f;
                return robotUp+robotDown;
            case 1:
                robotUp = 1;
                robotDown = 0.2f;
                return robotUp + robotDown;
            case 2:
                robotUp = 1;
                robotDown = 0.3f;
                return robotUp + robotDown;
            case 3:
                robotUp = 2;
                robotDown = 0.1f;
                return robotUp + robotDown;
            case 4:
                robotUp = 2;
                robotDown = 0.2f;
                return robotUp + robotDown;
            case 5:
                robotUp = 2;
                robotDown = 0.3f;
                return robotUp + robotDown;
            case 6:
                robotUp = 3;
                robotDown = 0.1f;
                return robotUp + robotDown;
            case 7:
                robotUp = 3;
                robotDown = 0.2f;
                return robotUp + robotDown;
            case 8:
                robotUp = 3;
                robotDown = 0.3f;
                return robotUp + robotDown;
        }
        return 0.0f;
    }
    public Color ColorId()
    {
        switch (randomIndexColor)
        {
            case 0:
                numberClock = 15;
                return Color.yellow;
            case 1:
                numberClock = 30;
                return Color.yellow;
            case 2:
                numberClock = 45;
                return Color.yellow;
            case 3:
                numberClock = 15;
                return Color.blue;
            case 4:
                numberClock = 30;
                return Color.blue;
            case 5:
                numberClock = 45;
                return Color.blue;
            case 6:
                numberClock = 15;
                return Color.green;
            case 7:
                numberClock = 30;
                return Color.green;
            case 8:
                numberClock = 45;
                return Color.green;
            case 9:
                numberClock = 15;
                return Color.red;
            case 10:
                numberClock = 30;
                return Color.red;
            case 11:
                numberClock = 45;
                return Color.red;
        }
        return Color.white;
    }
}
