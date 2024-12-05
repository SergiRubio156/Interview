using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseManager : MonoBehaviour
{
    public Camera mainCamera;
    public ObjectManager objectManager;

    public bool objectSelect;
    public GameObject objectHand;
    public GameObject objectButtonDoor;
    float velocity = 5f;

    public GameObject poisitionHand;
    public GameObject positionMachine;

    //Objeto cogido
    public Material outline;
    public GameObject objectOutline;
    public bool oneTime = false;

    float distanceMax;

    public AnimationArm animationArm;

    bool isPlaying;
    //int layerSpawn;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;   //Esto es el evento del script GameManager
        mainCamera = Camera.main;
        //controls = new StarterAssetsInputs();
    }

    void GameManager_OnGameStateChanged(GameState state)        //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
    {
        switch (state)
        {
            case GameState.Lasers:
                isPlaying = true;
                break;
            case GameState.Playing:
                isPlaying = false;
                break;
            case GameState.Settings:
                isPlaying = true;
                break;
            case GameState.Menu:
                break;
            case GameState.Wire:
                isPlaying = true;
                break;
            case GameState.Topping:
                isPlaying = true;
                break;
            case GameState.Color:
                isPlaying = true;
                break;
            case GameState.Exit:
                // Acciones a realizar cuando el estado de juego es "Exit"
                break;
        }
    }

    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            //layerSpawn = 1 << 14;
            CheckGround();
            if (objectSelect)
                distanceMax = Mathf.Infinity;
            else
                distanceMax = 5;
        }
    }

    void CheckGround()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit, distanceMax, LayerMask.NameToLayer("NoInteractable")))
        {
            Vector3 curScreenPoint = new Vector3(hit.point.x, hit.point.y + 0.8f, hit.point.z);
            transform.position = curScreenPoint;

            if ((hit.collider.tag == "Interactable" || hit.collider.tag == "Door") && !objectSelect)
            {
                outline = hit.collider.gameObject.GetComponentInChildren<MeshRenderer>().material;
                outline.SetFloat("_Outline_Thickness", 0.01f);
            }
            else if ((hit.collider.tag != "Interactable" || hit.collider.tag == "Door") && !objectSelect)
            {
                outline.SetFloat("_Outline_Thickness", 0f);
            }

            if (objectSelect)
            {
                Vector3 newPosition2 = Vector3.Lerp(objectHand.transform.position, poisitionHand.transform.position, Time.deltaTime * 50);
                objectHand.transform.position = newPosition2;//  * Time.deltaTime;

                if (hit.collider.tag == "Spawner")
                {
                    outline = hit.collider.gameObject.GetComponentInChildren<MeshRenderer>().material;
                    outline.SetColor("_Outline_Color", Color.green);

                }
                else if (hit.collider.tag != "Spawner")
                {
                    if(objectHand.tag != "Interactable")
                        outline.SetColor("_Outline_Color", Color.white);
                    positionMachine = null;
                }
            }
            else if (positionMachine != null)
            {

                if (positionMachine.name != "Cinta" && positionMachine.name != "Toppings")
                {
                    Vector3 newPosition2 = positionMachine.transform.position - objectHand.transform.position;
                    objectHand.transform.position += newPosition2;//  * Time.deltaTime;

                }
                else
                {
                    Vector3 newPosition = positionMachine.transform.GetChild(0).transform.position;
                    Vector3 newPosition2 = newPosition - objectHand.transform.position;
                    objectHand.transform.position += newPosition2;//  * Time.deltaTime;
                }
                StartCoroutine(Wait());
            }

            if (Input.GetMouseButtonDown(0))
            {
                Tuple<GameObject, string> objects = HitGameObject();
                if (objects != null)
                {
                    SwitchList(objects.Item1, objects.Item2);
                }
            }
        }
    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        positionMachine = null;
        objectHand = null;
    }

    Tuple<GameObject,string> HitGameObject()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        string _tag = "";
        GameObject _object = null;

        if (Physics.Raycast(ray, out hit, 5))
        {
            _tag = hit.collider.tag;
            _object = hit.collider.gameObject;

            return Tuple.Create(_object, _tag);
        }

        return Tuple.Create(_object, _tag);
    }

    void positionHands(float num)
    {
        if(num == 2)
        {
            poisitionHand.transform.localPosition = new Vector3(0.309f, 0.76f, 1.185f);
        }
        else
        {
            poisitionHand.transform.localPosition = new Vector3(0.309f, 1.316f, 1.185f);
        }
    }

    void SwitchList(GameObject _object,string _tag)
    {
        switch(_tag)
        {
            case "Interactable":
                if (!objectSelect)
                {
                    objectManager = _object.GetComponent<Objects>();
                    int i = objectManager.GetObjectPositionInList(_object);
                    if (i != -1)
                    {
                        if (objectManager.GetObjectStateInList(i) != ObjectState.Taked)
                        {
                            if (outline != null)
                                outline.SetFloat("_Outline_Thickness", 0f);
                            objectManager.ObjectGameState(i, ObjectState.Taked);
                            positionHands(_object.GetComponent<Objects>().robotUp);
                            objectHand = _object;
                            objectSelect = true;
                        }
                    }
                }
                break;
            case "Spawner":
                if (objectSelect)
                {

                    if (_object.name == "Wire")
                    {
                        Objects _obj = objectManager.FindStateOfObject(ObjectState.Taked);

                        int w = objectManager.GetObjectPositionInList(objectHand);
                        if (w != -1)
                        {

                            if (!objectManager.GetObjectBoolInList(w, "Wire"))
                            {
                                positionMachine = animationArm.GetRobotPosition(_obj.robotUp);
                                objectSelect = false;
                                objectManager.ObjectGameState(w, ObjectState.Arm);
                            
                            }
                        }
                    }
                    else if (_object.name == "Color")
                    {
                        Objects _obj = objectManager.FindStateOfObject(ObjectState.Taked);

                        int w = objectManager.GetObjectPositionInList(objectHand);
                        if (w != -1)
                        {
                            if (_obj.cablesCheck)
                            {
                                if (!objectManager.GetObjectBoolInList(w, "Color"))
                                {
                                    positionMachine = _object;
                                    objectSelect = false;
                                    objectManager.ObjectGameState(w, ObjectState.Color);
                                }
                            }
                        }
                    }
                    else if (_object.name == "Toppings")
                    {
                        int w = objectManager.GetObjectPositionInList(objectHand);
                        if (w != -1)
                        {
                            if (!objectManager.GetObjectBoolInList(w, "Toppings"))
                            {
                                positionMachine = _object;
                                objectSelect = false;
                                objectManager.ObjectGameState(w, ObjectState.Toppings);
                            }
                        }
                    }
                    else if (_object.name == "Cinta")
                    {
                        int w = objectManager.GetObjectPositionInList(objectHand);
                        if (w != -1)
                        {
                            positionMachine = _object;
                            objectSelect = false;
                            objectManager.ObjectGameState(w, ObjectState.Cinta);
                            _object.GetComponent<Cintamovement>().BoolCinta();
                        }
                    }
                }
                else
                {
                    if (_object.name == "Plancha")
                    {
                        GameManager.Instance.State = GameState.RobotPanel;
                    }
                    else if (_object.name == "Horno")
                    {
                        Objects _obj = objectManager.FindStateOfObject(ObjectState.Color);
                        int w = objectManager.GetObjectPositionInList(_obj.gameObject);

                        if (_obj != null)
                        {
                            objectManager.ObjectGameState(w, ObjectState.Taked);
                            objectHand = _obj.obj;
                            objectSelect = true;
                        }
                    }
                    else if (_object.name == "Wire")
                    {
                        if (animationArm.finishAnimation)
                        {
                        
                            Objects _obj = objectManager.FindStateOfObject(ObjectState.Arm);
                            int w = objectManager.GetObjectPositionInList(_obj.gameObject);

                            if (!_obj.cablesCheck)
                            {
                                objectManager.ObjectGameState(w, ObjectState.Cables);
                            }
                            else
                            {
                                objectManager.ObjectGameState(w, ObjectState.Taked);
                                objectHand = _obj.obj;
                                objectSelect = true;
                            }
                        }
                    }
                    else if (_object.name == "Toppings")
                    {
                        Objects _obj = objectManager.FindStateOfObject(ObjectState.Toppings);

                        int w = objectManager.GetObjectPositionInList(_obj.gameObject);

                        if (_obj.toppingCheck)
                        {
                            objectManager.ObjectGameState(w, ObjectState.Taked);
                            objectHand = _obj.gameObject;
                            objectSelect = true;
                        }
                    }
                }
                break;
               
            case "Door":
                _object.GetComponent<TransitionCamera>().transitionScene("Door");
                break;

            default:
                if(objectSelect)
                {
                    Objects _obj = objectManager.FindStateOfObject(ObjectState.Taked);

                    int d = objectManager.GetObjectPositionInList(objectHand);

                    if (d != -1)
                    {
                        if (_obj.state == ObjectState.Taked)
                        {
                            objectManager.ObjectGameState(d, ObjectState.NoTaked);
                            objectSelect = false;
                            objectHand = null;
                        }
                    }
                }
                break;
        }
    }
}
