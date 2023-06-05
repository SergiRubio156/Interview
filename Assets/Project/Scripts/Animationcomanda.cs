using UnityEngine;
using UnityEngine.UI;

public class Animationcomanda : MonoBehaviour
{
    public Animator animatorController;
    public string animationName = "NombreDeLaAnimacion";
    public string animationclose = "NombreDeLaAnimacion";
    //public Button button;

    private void Start()
    {
        // Aseg�rate de asignar el componente Animator al objeto en el Inspector de Unity.
        if (animatorController == null)
        {
            animatorController = GetComponent<Animator>();
        }

        // Asigna el m�todo PlayAnimation al evento OnClick del bot�n.
        //button.onClick.AddListener(PlayAnimation);
    }

    public void PlayAnimation()
    {
        // Activa el trigger correspondiente al nombre de la animaci�n.
        animatorController.SetTrigger(animationName);
    }
    public void PlayAnimation2()
    {
        // Activa el trigger correspondiente al nombre de la animaci�n.
        animatorController.SetTrigger(animationclose);
    }
}
