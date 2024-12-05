using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Adaptacion : MonoBehaviour
{
    public TMP_Text textoCronometro;
    public GameObject panelParar; // Variable para controlar el panel

    public float tiempoTranscurrido = 0f;
    private bool cronometroActivo = true; // Variable para controlar si el cronómetro está activo o no
    private bool cronometroDetenido = false; // Variable para controlar si el cronómetro ha sido detenido

    void Update()
    {
        if (cronometroActivo && !cronometroDetenido)
        {
            tiempoTranscurrido += Time.deltaTime;
            ActualizarTextoCronometro();

            if (panelParar != null && panelParar.activeInHierarchy)
            {
                DetenerCronometro();
            }
        }
    }

    void ActualizarTextoCronometro()
    {
        if (textoCronometro != null)
        {
            int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60f);
            int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60f);

            textoCronometro.text = minutos.ToString("0") + ":" + segundos.ToString("00") + " minutos";
        }
    }

    // Método para detener el cronómetro
    public void DetenerCronometro()
    {
        cronometroActivo = false;
        cronometroDetenido = true;
    }
    public float ObtenerTiempoTranscurrido()
    {
        return tiempoTranscurrido;
    }

}
