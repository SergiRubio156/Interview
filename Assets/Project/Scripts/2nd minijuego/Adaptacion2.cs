using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Adaptacion2 : MonoBehaviour
{
    public TMP_Text textoCronometro;
    public GameObject panelRobots; // Variable para controlar el panel "robots"
    public GameObject panelTerminar; // Variable para controlar el panel "terminar"

    public float tiempoTranscurrido2 = 0f; // Ahora la variable es pública

    private bool cronometroActivo = false; // Variable para controlar si el cronómetro está activo o no
    private bool cronometroDetenido = false; // Variable para controlar si el cronómetro ha sido detenido

    void Update()
    {
        if (!cronometroDetenido)
        {
            if (panelRobots != null && panelRobots.activeInHierarchy)
            {
                cronometroActivo = true;
            }

            if (cronometroActivo)
            {
                tiempoTranscurrido2 += Time.deltaTime;
                ActualizarTextoCronometro();
            }

            if (panelTerminar != null && panelTerminar.activeInHierarchy)
            {
                DetenerCronometro();
            }
        }
    }

    void ActualizarTextoCronometro()
    {
        if (textoCronometro != null)
        {
            int minutos = Mathf.FloorToInt(tiempoTranscurrido2 / 60f);
            int segundos = Mathf.FloorToInt(tiempoTranscurrido2 % 60f);

            textoCronometro.text = minutos.ToString("0") + ":" + segundos.ToString("00") + " minutos";
        }
    }

    // Método para detener el cronómetro
    public void DetenerCronometro()
    {
        cronometroActivo = false;
        cronometroDetenido = true;
    }
}
