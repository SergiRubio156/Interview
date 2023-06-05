using UnityEngine;
using UnityEngine.UI;

public class Animationcomanda : MonoBehaviour
{
    public Animator animatorController;
    public string animationOpen = "Abrir";
    public string animationClose = "Cerrar";
    public Button openButton;
    public Button closeButton;
    public GameObject paneltuto; 

    private void Start()
    {
        openButton.gameObject.SetActive(false);
        paneltuto.gameObject.SetActive(true);
        // Aseg�rate de asignar el componente Animator al objeto en el Inspector de Unity.
        if (animatorController == null)
        {
            animatorController = GetComponent<Animator>();
        }

        // Asigna el m�todo PlayAnimation a los eventos OnClick de los botones.
        openButton.onClick.AddListener(PlayOpenAnimation);
        closeButton.onClick.AddListener(PlayCloseAnimation);
    }

    public void PlayOpenAnimation()
    {
        // Activa el trigger correspondiente a la animaci�n de abrir.
        animatorController.SetTrigger(animationOpen);

        // Desactiva el bot�n de abrir.
        openButton.gameObject.SetActive(false);

        // Activa el bot�n de cerrar.
        closeButton.gameObject.SetActive(true);
    }

    public void PlayCloseAnimation()
    {
        // Activa el trigger correspondiente a la animaci�n de cerrar.
        animatorController.SetTrigger(animationClose);

        // Desactiva el bot�n de cerrar.
        closeButton.gameObject.SetActive(false);

        // Activa el bot�n de abrir.
        openButton.gameObject.SetActive(true);
    }
}
