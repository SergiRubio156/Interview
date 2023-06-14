using UnityEngine;

public class Win1 : MonoBehaviour
{
    public GameObject pesta�a; // Referencia al GameObject de la pesta�a a activar para "Door"
    public GameObject pesta�aLevel2; // Referencia al GameObject de la pesta�a a activar para "Level2"

    private bool pesta�aActivada = false; // Variable para controlar si la pesta�a est� activada o no

    private CharacterController characterController; // Referencia al CharacterController adjunto al personaje

    private void Start()
    {
        pesta�a.SetActive(false); // Desactiva la pesta�a para "Door"
        pesta�aLevel2.SetActive(false); // Desactiva la pesta�a para "Level2"

        // Obt�n la referencia al CharacterController adjunto al GameObject del personaje
        characterController = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Comprueba si el personaje colisiona con el objeto etiquetado como "Door"
        if (hit.collider.CompareTag("Door") && !pesta�aActivada)
        {
            pesta�a.SetActive(true); // Activa la pesta�a para "Door"
            pesta�aActivada = true; // Marca la pesta�a como activada
        }

        // Comprueba si el personaje colisiona con el objeto etiquetado como "Level2"
        if (hit.collider.CompareTag("Level2"))
        {
            pesta�aLevel2.SetActive(true); // Activa la pesta�a para "Level2"
        }
    }

    private void Update()
    {
        // Comprueba si el personaje ya no est� colisionando con el objeto "Door"
        if (pesta�aActivada && !IsCollidingWithDoor())
        {
            pesta�a.SetActive(false); // Desactiva la pesta�a para "Door"
            pesta�aActivada = false; // Marca la pesta�a como desactivada
        }

        // Comprueba si el personaje ya no est� colisionando con el objeto "Level2"
        if (!IsCollidingWithLevel2())
        {
            pesta�aLevel2.SetActive(false); // Desactiva la pesta�a para "Level2"
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

    private bool IsCollidingWithLevel2()
    {
        // Comprueba si el personaje est� colisionando con un objeto etiquetado como "Level2"
        Collider[] colliders = Physics.OverlapSphere(transform.position, characterController.radius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Level2"))
            {
                return true;
            }
        }

        return false;
    }
}
