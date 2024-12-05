using UnityEngine;

public class OtherScript : MonoBehaviour
{
    private Win1 win1Script; // Referencia al script Win1

    private void Start()
    {
        win1Script = GetComponent<Win1>(); // Obtén la referencia al script Win1 adjunto al GameObject

        win1Script.enabled = false; // Desactiva el script Win1 al inicio
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Comprueba si la colisión es con el objeto etiquetado como "Door"
        if (collision.collider.CompareTag("Door"))
        {
            win1Script.enabled = true; // Activa el script Win1
        }
    }
}
