using UnityEngine;

public class CountdownStarter : MonoBehaviour
{
    public TimerController timerController; // Referencia al script TimerController

    private void Start()
    {
        // Obtén la referencia al script TimerController
        timerController = GetComponent<TimerController>();

        // Verifica que la referencia sea válida
        if (timerController != null)
        {
            // Inicia la cuenta regresiva llamando al método StartCountdown()
            timerController.StartCountdown();
        }
        else
        {
            Debug.LogError("La referencia al TimerController no está asignada en CountdownStarter");
        }
    }
}
