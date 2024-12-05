using UnityEngine;
using TMPro;

public class CalculadoraMedia : MonoBehaviour
{
    public Adaptacion2 scriptAdaptacion2;
    public Adaptacion scriptAdaptacion;
    public TMP_Text textoMediaTiempoTranscurrido; // Referencia al objeto de texto

    void Update()
    {
        float tiempoTranscurrido1 = scriptAdaptacion.tiempoTranscurrido;
        float tiempoTranscurrido2 = scriptAdaptacion2.tiempoTranscurrido2;

        float media = (tiempoTranscurrido1 + tiempoTranscurrido2) / 2f;

        MostrarMediaTiempoTranscurrido(media);
    }

    void MostrarMediaTiempoTranscurrido(float mediaTiempoTranscurrido)
    {
        if (textoMediaTiempoTranscurrido != null)
        {
            int minutos = Mathf.FloorToInt(mediaTiempoTranscurrido / 60f);
            int segundos = Mathf.FloorToInt(mediaTiempoTranscurrido % 60f);

            textoMediaTiempoTranscurrido.text = " " + minutos.ToString("0") + ":" + segundos.ToString("00") + " minutos";
        }
    }
}
