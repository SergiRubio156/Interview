using UnityEngine;

public class CountdownStarter : MonoBehaviour
{
    public TimerController timerController; // Referencia al script TimerController

    private void Start()
    {
        // Obt�n la referencia al script TimerController
        timerController = GetComponent<TimerController>();

        // Verifica que la referencia sea v�lida
        if (timerController != null)
        {
            // Inicia la cuenta regresiva llamando al m�todo StartCountdown()
            timerController.StartCountdown();
        }
        else
        {
            Debug.LogError("La referencia al TimerController no est� asignada en CountdownStarter");
        }
    }
}
