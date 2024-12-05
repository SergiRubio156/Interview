using UnityEngine;
using UnityEngine.UI;

public class CerrarPantalla : MonoBehaviour
{
    public GameObject pantalla;
    public GameObject abrirbotton;
    public Button cerrarControl;
    public Button abrirControl;
    private bool pantallaVisible;

    public TimeManager timeManager;
    // contador veces preguntar
    public int questionCont = 0;
    bool oneTime;
    void Start()
    {
        if (!oneTime)
        {
            pantalla.SetActive(true);
            timeManager.totalHelpLvl1++;
            oneTime = true;
        }
        else if(oneTime)
        {
            pantalla.SetActive(false);
        }
        abrirbotton.SetActive(true);
        pantallaVisible = false;
        cerrarControl.onClick.AddListener(CruzPantalla); 
        abrirControl.onClick.AddListener(interrogantePantalla); 

    }

    void interrogantePantalla()
    {
        timeManager.totalHelpLvl1++;
        pantallaVisible = !pantallaVisible;
        pantalla.SetActive (true);
        abrirbotton.SetActive (false);
    }    
    
    void CruzPantalla()
    {
        pantallaVisible = !pantallaVisible;
        abrirbotton.SetActive(true);
        pantalla.SetActive (false);
    

    }

}
