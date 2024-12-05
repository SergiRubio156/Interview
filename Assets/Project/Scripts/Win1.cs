using UnityEngine;

public class Win1 : MonoBehaviour
{
    public GameObject pesta�a; // Referencia al GameObject de la pesta�a a activar

    private bool pesta�aActivada = false; // Variable para controlar si la pesta�a est� activada o no

    private CharacterController characterController; // Referencia al CharacterController adjunto al personaje

    private void Start()
    {
        pesta�a.SetActive(false); // Desactiva la pesta�a al inicio

        // Obt�n la referencia al CharacterController adjunto al GameObject del personaje
        characterController = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Comprueba si el personaje colisiona con el objeto etiquetado como "Door"
        if (hit.collider.CompareTag("Door") && !pesta�aActivada)
        {
            pesta�a.SetActive(true); // Activa la pesta�a
            pesta�aActivada = true; // Marca la pesta�a como activada
        }
    }


    private void Update()
    {
        // Comprueba si el personaje ya no est� colisionando con el objeto
        if (pesta�aActivada && !IsCollidingWithDoor())
        {
            pesta�a.SetActive(false); // Desactiva la pesta�a
            pesta�aActivada = false; // Marca la pesta�a como desactivada
        }
    }

    private bool IsCollidingWithDoor()
    {
        // Comprueba si el personaje est� colisionando con un objeto etiquetado como "Door"
        Collider[] colliders = Physics.OverlapSphere(transform.position, characterController.radius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Door"))
            {
                return true;
            }
        }

        return false;
    }
}
