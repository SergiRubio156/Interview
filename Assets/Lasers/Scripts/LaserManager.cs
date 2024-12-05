using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserManager : MonoBehaviour
{
    //private StarterAssetsInputs controls;
    public Camera mainCamera = null;
    public GameObject ObjectMove;
    public Transform objectSun;

    //Raycast
    RaycastHit hit;
    Vector3 curScreenPoint;
    //Mouse
    bool sceneSettings;
    Vector3 worldPosition;
    bool objectSelect;
    public float velocity = 5f;
    int LayerPlane;

    //Rotacion 
    public float rotationSpeed = 1f;
    Quaternion currentRotation;
    public float targetRotation = 45f;
        
    //Rigidbody
    Rigidbody rb = null;
    bool isPlaying;
    float positionIntial = 0f;

    void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;   //Esto es el evento del script GameManager                //controls = new StarterAssetsInputs();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;   //La funcion "OnDestroy" se activa cuando destruimos el objeto, una vez destruido se activa el evento,
    }
    private void Start()
    {
        objectSelect = false;
    }

    private void HandleGameStateChanged(GameState newState)        //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {

        switch (newState)
        {
            case GameState.Playing:
                isPlaying = false;
                break;
            case GameState.Lasers:
                objectSelect = false;
                ObjectMove = null;
                mainCamera = GameObject.FindGameObjectWithTag("CameraLvL").gameObject.GetComponent<Camera>();
                isPlaying = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Settings:
                break;
            case GameState.Menu:
                break;
            case GameState.Wire:
                break;
            case GameState.Exit:
                // Acciones a realizar cuando el estado de juego es "Exit"
                break;
        }
    }



    void Update()
    {
        LayerPlane = 1 << 13;

        if (isPlaying)
        {
            CheckGround();
        }
    }


    void CheckGround()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerPlane))
        {

            if (Input.GetMouseButtonDown(0))
                RayObject();

            if (objectSelect)
            {
                if (ObjectMove != null)
                {
                    if (positionIntial >= ObjectMove.transform.position.y)
                    {
                        Vector3 newPosition = new Vector3(hit.point.x, positionIntial, hit.point.z);
                        Vector3 velocitys = (newPosition - ObjectMove.transform.position) * velocity;
                        ObjectMove.GetComponent<Rigidbody>().velocity = velocitys;
                        currentRotation = ObjectMove.transform.rotation;
                    }
                }
                else if(objectSun != null)
                    currentRotation = objectSun.rotation;
                Rotation();
            }

        }
    }

    void Rotation()
    {
        Quaternion targetQuaternion = Quaternion.Euler(0, targetRotation, 0) * currentRotation;

        if (Input.GetKey(KeyCode.E))
        {
            targetQuaternion = Quaternion.Euler(0, targetRotation, 0) * currentRotation;
            ApplyRotation(targetQuaternion);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            targetQuaternion = Quaternion.Euler(0, -targetRotation, 0) * currentRotation;
            ApplyRotation(targetQuaternion);
        }
    }

    void ApplyRotation(Quaternion targetRotation)
    {
        Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (objectSun != null)
            objectSun.rotation = newRotation;
        else if (ObjectMove != null)
            ObjectMove.transform.rotation = newRotation;
    }
    void RayObject()
    {

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && !objectSelect)
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                // Ajuste de la posición del rayo del mouse
                Vector3 objectToHit = hit.point - hit.collider.gameObject.transform.position;
                ray = new Ray(hit.collider.gameObject.transform.position, objectToHit.normalized);
            }
        }
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && !objectSelect)
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Debug.Log(hit.collider.gameObject.layer);
                if (hit.collider.gameObject.layer == 7)
                {
                    objectSun = hit.collider.transform.Find("LaserSun");
                }
                else if (hit.collider.gameObject.layer == 8)
                {
                    ObjectMove = hit.collider.transform.parent.gameObject;
                }
                else if (hit.collider.gameObject.layer == 9)
                {
                    ObjectMove = hit.collider.transform.parent.gameObject;
                }
                else 
                {
                    ObjectMove = hit.collider.gameObject;
                }
                positionIntial = ObjectMove.transform.position.y;
                rb = ObjectMove.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
                objectSelect = true;
            }
        }
        else if(objectSelect)
        {
            if(ObjectMove != null)
            {
                ObjectMove.transform.position = new Vector3(ObjectMove.transform.position.x, positionIntial, ObjectMove.transform.position.z);
                ObjectMove.GetComponent<Rigidbody>().velocity = ObjectMove.transform.position * 0f;
                ObjectMove = null;
            }
            if (objectSun != null)
            {
                objectSun = null;
            }
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb = null;
            objectSelect = false;
        }
    }
    
}
