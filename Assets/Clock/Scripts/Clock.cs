using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour
{

    //-- set start time 00:00
    public int minutes = 0;
    public bool isPlaying,robotIn;

    public GameObject pointerMinutes;
    public Objects obj;
    ObjectManager objectManager;

    //-- time speed factor
    public float clockSpeed = 1.0f;     // 1.0f = realtime, < 1.0f = slower, > 1.0f = faster

    //-- internal vars
    float msecs = 0;

    private void OnEnable()
    {
        objectManager = FindObjectOfType<ObjectManager>();
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }
    private void HandleGameStateChanged(GameState newState)    //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {
        switch (newState)
        {
            case GameState.Playing:
                isPlaying = true;
                break;
            case GameState.Lasers:
                break;
            case GameState.Settings:
                isPlaying = false;
                break;
            case GameState.Menu:
                isPlaying = false;
                break;
            case GameState.Wire:
                break;
            case GameState.Topping:
                break;
            case GameState.Color:
                obj = objectManager.FindStateOfObject(ObjectState.Color);
                isPlaying = true;
                robotIn = true;
                break;
            case GameState.Exit:
                // Acciones a realizar cuando el estado de juego es "Exit"
                break;
        }
    }
    void Update()
    {
        if (isPlaying)
        {
            if (robotIn)
            {
                if (obj.colorCheck)
                {
                    StartCoroutine(WaitColor());
                }
                else
                {
                    pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    if (minutes != 0)
                    {
                        isPlaying = false;
                        robotIn = false;
                        obj.timeColor = -minutes;
                        minutes = 0;
                    }
                }
            }
        }

    }
    private IEnumerator WaitColor()
    {
        
        yield return new WaitUntil(() => obj.colorCheck);
        //-- calculate time
        msecs += Time.deltaTime * clockSpeed;
        if (msecs >= 1.0f)
        {
            msecs -= 1.0f;
            minutes--;
        }

        float rotationMinutes = (360.0f / 60.0f) * minutes;

        pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationMinutes);
        if(minutes <= -60)
        {
            obj.colorCheck = false;
            robotIn = false;
        }
    }
}
