using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tiempo : MonoBehaviour
{
    public TextMeshProUGUI Numeropulsado;
    public TextMeshProUGUI mediaText; // Texto para mostrar la media de los segundos pasados
    public TextMeshProUGUI scaledMediaText; // Texto para mostrar la media escalada
    public int ammountLaserPuzzleSkipped = 0;
    public KeyCode keyToSkipPuzzle = KeyCode.M;

    // Contador del tiempo hasta entre "M"
    public float puzzleSkippedCont = 0;
    public bool startPuzzleCont = false;
    public List<float> timePuzzleCont = new List<float>();

    private int buttonPressCount = 0; // Variable para almacenar el número de veces que se presiona el botón

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToSkipPuzzle))
        {
            IncreaseSkippedAmmount();
            buttonPressCount++; // Incrementar el contador de presiones de botón
        }

        if (startPuzzleCont)
        {
            puzzleSkippedCont += Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SwitchPuzzleCounterBool(true);
        }

        Numeropulsado.text = " " + buttonPressCount.ToString(); // Mostrar el número de veces que se presionó el botón

        float mediaSegundos = CalculateAverageSeconds(); // Calcular la media de los segundos pasados
        int mediaMinutos = (int)mediaSegundos / 60;
        int mediaSegundosRestantes = (int)mediaSegundos % 60;
        mediaText.text = " " + mediaMinutos.ToString() + ":" + mediaSegundosRestantes.ToString("00");

        float scaledMedia = ScaleMedia(buttonPressCount); // Escalar la media de las veces pulsadas
        scaledMediaText.text =  scaledMedia.ToString("0") + " % ";
    }

    public void IncreaseSkippedAmmount()
    {
        ammountLaserPuzzleSkipped++;
        timePuzzleCont.Add(puzzleSkippedCont);
        puzzleSkippedCont = 0;
    }

    public void SwitchPuzzleCounterBool(bool state)
    {
        startPuzzleCont = state;
    }

    private float CalculateAverageSeconds()
    {
        if (timePuzzleCont.Count == 0)
            return 0;

        float sum = 0;
        foreach (float time in timePuzzleCont)
        {
            sum += time;
        }

        return sum / timePuzzleCont.Count;
    }

    private float ScaleMedia(int count)
    {
        float scaledValue = (count / 9.0f) * 100; // Escalar la media al rango de 0 a 100
        scaledValue = Mathf.Clamp(scaledValue, 0, 100); // Asegurarse de que la media escalada está dentro del rango

        return scaledValue;
    }
}
