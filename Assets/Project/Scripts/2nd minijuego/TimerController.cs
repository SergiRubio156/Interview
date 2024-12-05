using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public float countdownTime = 300f; // Tiempo inicial en segundos (5 minutos)
    public int targetPresses = 10; // Número objetivo de pulsaciones de "A"
    private float elapsedTime = 0f; // Tiempo transcurrido
    private bool isCountingDown = false; // Indicador de si se está realizando la cuenta regresiva
    private List<string> recordedTimes = new List<string>(); // Lista de los tiempos registrados
    public int timesPressedA = 0; // Contador de cuántas veces se ha presionado la tecla "A"

    public TextMeshProUGUI tiempo; // Referencia al objeto de texto para la cuenta regresiva
    public TextMeshProUGUI media; // Referencia al objeto de texto para mostrar la media
    public TextMeshProUGUI robotshechos; // Referencia al objeto de texto para mostrar el recuento de pulsaciones de "A"
    public TextMeshProUGUI porcentaje; // Referencia al objeto de texto para mostrar el porcentaje de pulsaciones

    public void StartCountdown()
    {
        isCountingDown = true;
        elapsedTime = 0f;
        recordedTimes.Clear();
        timesPressedA = 0;
    }

    private void RecordElapsedTime()
    {
        if (isCountingDown)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            string timeInClockFormat = minutes.ToString() + ":" + seconds.ToString("00"); // Convertir a formato de reloj (0:00)

            recordedTimes.Add(timeInClockFormat);
        }
    }

    private void Update()
    {
        if (isCountingDown)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= countdownTime)
            {
                isCountingDown = false;
                Debug.Log("¡Tiempo límite alcanzado!");
                CalculateAverageTime();
                CalculatePercentage();
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            RecordElapsedTime();
            timesPressedA++;
        }

        // Mostrar el tiempo transcurrido en formato de reloj (0:00)
        int remainingSeconds = Mathf.CeilToInt(countdownTime - elapsedTime);
        int minutes = remainingSeconds / 60;
        int seconds = remainingSeconds % 60;
        string timeString = minutes.ToString() + ":" + seconds.ToString("00"); // Convertir a formato de reloj (0:00)

        tiempo.text = timeString; // Actualizar el texto en el objeto de texto para la cuenta regresiva
        robotshechos.text = timesPressedA.ToString() + " "; // Actualizar el texto en el objeto de texto para mostrar el recuento de pulsaciones de "A"
    }

    private void CalculateAverageTime()
    {
        if (recordedTimes.Count > 0)
        {
            float totalSeconds = 0f;

            foreach (string timeString in recordedTimes)
            {
                string[] timeParts = timeString.Split(':');
                int minutes = int.Parse(timeParts[0]);
                int seconds = int.Parse(timeParts[1]);
                float timeInSeconds = minutes * 60 + seconds;
                totalSeconds += timeInSeconds;
            }

            float averageTimeInSeconds = totalSeconds / recordedTimes.Count;
            int averageMinutes = Mathf.FloorToInt(averageTimeInSeconds / 60);
            int averageSeconds = Mathf.FloorToInt(averageTimeInSeconds % 60);
            string averageTimeString = averageMinutes.ToString() + ":" + averageSeconds.ToString("00"); // Convertir a formato de reloj (0:00)

            media.text = " " + averageTimeString; // Actualizar el texto en el objeto de texto para mostrar la media
        }
        else
        {
            media.text = "Media: N/A"; // No hay tiempos registrados, mostrar "N/A" como media
        }
    }

    private void CalculatePercentage()
    {
        int percentage = (timesPressedA * 100) / targetPresses;
        porcentaje.text = " " + percentage.ToString() + "%"; // Actualizar el texto en el objeto de texto para mostrar el porcentaje
    }
}
