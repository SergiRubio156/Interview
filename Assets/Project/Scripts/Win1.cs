using UnityEngine;

public class Win1 : MonoBehaviour
{
    public GameObject pestaña; // Referencia al GameObject de la pestaña a activar

    private bool pestañaActivada = false; // Variable para controlar si la pestaña está activada o no

    private CharacterController characterController; // Referencia al CharacterController adjunto al personaje

    private void Start()
    {
        pestaña.SetActive(false); // Desactiva la pestaña al inicio

        // Obtén la referencia al CharacterController adjunto al GameObject del personaje
        characterController = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Comprueba si el personaje colisiona con el objeto etiquetado como "Door"
        if (hit.collider.CompareTag("Door") && !pestañaActivada)
        {
            pestaña.SetActive(true); // Activa la pestaña
            pestañaActivada = true; // Marca la pestaña como activada
        }
    }


    private void Update()
    {
        // Comprueba si el personaje ya no está colisionando con el objeto
        if (pestañaActivada && !IsCollidingWithDoor())
        {
            pestaña.SetActive(false); // Desactiva la pestaña
            pestañaActivada = false; // Marca la pestaña como desactivada
        }
    }

    private bool IsCollidingWithDoor()
    {
        // Comprueba si el personaje está colisionando con un objeto etiquetado como "Door"
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
