using UnityEngine;

public class ActivarObjetoColision : MonoBehaviour
{
    public CharacterController characterController; // Referencia al CharacterController del jugador
    public KeyCode teleportKey = KeyCode.R; // Tecla para activar el teletransportador
    public Transform teleportPoint; // Punto de teletransportaci�n
    public Transform originalPoint; // Punto original de regreso
    public Animator animator; // Referencia al componente Animator para reproducir la animaci�n
    public Vector3 destination;
    private bool myTeleported;

    private bool teleported = false; // Variable para controlar si el jugador se ha teletransportado

    private void Start()
    {
        animator = GameObject.Find("PanelAnimator").GetComponent<Animator>();
        myTeleported = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(teleportKey))
        {
            if (!teleported)
            {
                //TeleportToDestination(teleportPoint.position); // Teletransporta al jugador al punto de teletransportaci�n
                destination = teleportPoint.position;
                teleported = true; // Marca el jugador como teletransportado
                if (animator != null)
                {
                    animator.Play("teleport"); // Activa la animaci�n asignada al activador
                }
            }
            else
            {
                //TeleportToDestination(originalPoint.position); // Teletransporta al jugador al punto original
                destination = originalPoint.position;
                teleported = false; // Marca el jugador como no teletransportado
                if (animator != null)
                {
                    animator.Play("teleport"); // Activa la animaci�n asignada al activador
                }
            }
        }
    }

    public void TeleportToDestination()
    {
        characterController.enabled = false; // Deshabilita el CharacterController para evitar movimientos durante el teletransporte
        characterController.transform.position = destination; // Establece la posici�n del jugador al destino
        characterController.transform.rotation = Quaternion.identity; // Reinicia la rotaci�n del jugador
        characterController.enabled = true; // Habilita nuevamente el CharacterController
    }
    public void ChangeTeleportedValue()
    {
        myTeleported = !myTeleported;
    }
}
