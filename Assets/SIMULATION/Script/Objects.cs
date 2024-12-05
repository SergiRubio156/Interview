using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[System.Serializable]
public class Objects : ObjectManager
{

    public int id;
    public GameObject obj;
    public ObjectState state;
    public float numUp;
    public float numDown;
    public float robotUp;
    public float robotDown;

    public BoxCollider boxColliderUp;
    public Rigidbody rb;

    public bool cablesCheck = false;
    public bool toppingCheck = false;
    public bool finishMove = false;
    public bool colorChoose = false;
    bool isPlaying;
    //COLOR

    public bool colorCheck = false;
    float durationColor = 60f;
    public Gradient gradient;
    float time = 0f;
    public Renderer[] rend = new Renderer[1];
    public GameObject parts;
    public BoxCollider[] boxColliderDown;

    [SerializeField]
    private Color currentColor;
    public Color FinalColor;
    public int timeColor;
    public Material outline;
    MenuManager menuManager;

    private void Start()
    {
        obj = this.gameObject;
        boxColliderUp = GetComponent<BoxCollider>();
        rend = GetComponentsInChildren<Renderer>();
        outline = GetComponentInChildren<MeshRenderer>().material;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;
        menuManager = FindObjectOfType<MenuManager>();

    }
    private void OnEnable()
    {
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
                isPlaying = false;
                break;
            case GameState.Exit:
                // Acciones a realizar cuando el estado de juego es "Exit"
                break;
        }
    }
    public override void ObjectNoTaked()
    {
        boxColliderUp.enabled = true;
        if (parts != null)
            ForBoxCollider(true);
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;
        outline.SetFloat("_Outline_Thickness", 0.01f);
    }

    public override void ObjectTaked()
    {
        boxColliderUp.enabled = false;
        if (parts != null)
            ForBoxCollider(false);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        LerpRotation(robotUp);
        colorCheck = false;
    }
    public override void ObjectCables()
    {
            StartCoroutine(WaitWire());
    }
    public override void ObjectArm()
    {
        if (robotDown == 0)
        {
            //transform.rotation = Quaternion.Euler(0f, -103f, 0f);
            GameManager.Instance.State = GameState.ArmPanel;
        }
    }
    public override void ObjectToppings()
    {
        if (!toppingCheck)
        {
            toppingCheck = true;
            StartCoroutine(WaitToppings());
        }
    }

    public override void ObjectColors()
    {
        transform.rotation = Quaternion.Euler(0f, 75.624f, 0f);
        boxColliderUp.enabled = true;
        StartCoroutine(WaitColor());
    }
    public override void ObjectCinta()
    {
        boxColliderUp.enabled = true;
        if (parts != null)
            ForBoxCollider(true);
    }

    void ForBoxCollider(bool _bool)
    {
        for(int i = 0; i < boxColliderDown.Length; i++)
        {
            boxColliderDown[i].enabled = _bool;
        }
    }
    IEnumerator WaitToppings()
    {
        yield return new WaitForSeconds(0.2f);
        boxColliderUp.enabled = true;
        GameManager.Instance.State = GameState.Topping;

    }
    IEnumerator WaitWire()
    {
        yield return new WaitForSeconds(0.2f);
        parts = transform.GetChild(1).gameObject;
        parts.SetActive(true);
        boxColliderDown = parts.GetComponentsInChildren<BoxCollider>();
        rend = GetComponentsInChildren<Renderer>();
        boxColliderUp.enabled = true;
        GameManager.Instance.State = GameState.Wire;

    }
    private IEnumerator WaitColor()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.Instance.State = GameState.Color;

    }

    private void Update()
    {
        if(colorCheck && isPlaying)
        {
            LerpPosition();
        }
    }

    private void LerpPosition()
    {
        currentColor = gradient.Evaluate(time / durationColor);

        time += Time.deltaTime;


        // Combinar el color del gradiente y el color de la textura
        ForRenderer(currentColor);
    }
    void ForRenderer(Color currentColor)
    {
        for (int i = 0; i < rend.Length; i++)
        {
            rend[i].materials[0].color = currentColor;
        }
    }
    public void LerpRotation(float _int)
    {
        switch(_int)
        {
            case 1:
                transform.rotation = Quaternion.Euler(0, -12.312f, 0);
                break;
            case 2:
                transform.rotation = Quaternion.Euler(0, -102.39f, 0);
                break;
            case 3:
                transform.rotation = Quaternion.Euler(-89.59F, -105.97f, 4.25F);
                break;
        }
    }
}
