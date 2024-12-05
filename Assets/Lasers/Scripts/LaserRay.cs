using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class LaserRay : MonoBehaviour
{
    int layerWalls,layerMirror,layerCylinder,layerTriangle, LayerStart, LayerFinal;
    
    public LineRenderer inputLine;

    bool isPlaying;

    RaycastHit hit;

    //GameObjects
    [SerializeField]
    private GameObject cubeColor = null;
    [SerializeField]
    private GameObject reflexive = null;
    [SerializeField]
    private GameObject triangle = null;
    [SerializeField]
    private GameObject laserFinal = null;
    [SerializeField]
    public GameObject LaserObject = null;

    Vector3 positionLaser;
    //Rotacion 
    public float rotationSpeed = 0;
    private float anguloActual = 0f;
    public bool isRotation = true;
    //
    public RaycastLine raycastLine;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
        positionLaser = LaserObject.transform.position;
        inputLine = LaserObject.GetComponentInChildren<LineRenderer>();
        raycastLine = GameObject.FindGameObjectWithTag("RaycastLine").GetComponent<RaycastLine>();
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
                isPlaying = false;
                break;
            case GameState.Lasers:
                isPlaying = true;
                isRotation = true;
                break;
            case GameState.Settings:
                isRotation = false;
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
        layerMirror = 1 << 6;
        layerCylinder = 1 << 7;
        layerTriangle = 1 << 8;
        layerWalls = 1 << 9;
        LayerStart = 1 << 10;
        LayerFinal = 1 << 11;
        positionLaser = LaserObject.transform.position;
        if (isPlaying)
        {
            LaserDraw();
            ObjectRotate();
            if (Input.GetKeyDown(KeyCode.Space)) //Cuando le damos click al Escape entra a esta funcion
            {
                isRotation = !isRotation;// = true ? isRotation : !isRotation;
            }
        }
    }

    void ObjectRotate()
    {
        if (isRotation)
        {
            float rotacion = rotationSpeed * Time.deltaTime;
            anguloActual += rotacion;

            if (anguloActual >= 30f)
            {
                rotationSpeed = -rotationSpeed;
                anguloActual = 30f;
            }

            if (anguloActual <= -30f)
            {
                rotationSpeed = -rotationSpeed;
                anguloActual = -30f;
            }

            // Rotamos el objeto en el eje Y
            transform.Rotate(0f, 0f, rotacion);
        }


    }

    bool RotationSpeed()
    {
        if (rotationSpeed > 0)
        {
            return true;
        }
        return false;
    }

    void LaserMirror()
    {

        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(positionLaser, LaserObject.transform.forward, layerMirror, inputLine.material);

        if(reflexive != null)
        {
            if (reflexive != objects.Item1)
            {
                reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material, transform.position, null);
                reflexive = null;
            }
        }

        reflexive = objects.Item1;
        Vector3 _position = objects.Item2;
        string _name = objects.Item3;
        Material _mat = objects.Item4;
        Vector3 _posDir = objects.Item5;

        Vector3 reflectiveRayPoint = Vector3.Reflect(_position - transform.position, _posDir);

        inputLine.SetPosition(0, positionLaser);
        inputLine.SetPosition(1, _position);



        if (reflexive != null)
        {
            if (RotationSpeed())
            {
                rotationSpeed = 5;
            }
            else
            {
                rotationSpeed = -5;
            }
            reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(_position, reflectiveRayPoint, true, _mat, transform.position, this.gameObject);
        }
        laserReset("Mirror");
    }

    void LaserColor()
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(positionLaser, LaserObject.transform.forward, layerCylinder, inputLine.material);

        cubeColor = objects.Item1;
        Vector3 _position = objects.Item2;
        string _name = objects.Item3;
        Material _mat = objects.Item4;

        inputLine.SetPosition(0, positionLaser);
        inputLine.SetPosition(1, _position);

        if (cubeColor != null)
        {
            if (RotationSpeed())
            {
                rotationSpeed = 5;
            }
            else
            {
                rotationSpeed = -5;
            }
            cubeColor.GetComponentInParent<CubeColors>().RecivedColors(_mat, true, _name);
        }
        laserReset("Color");                
    }

    void LaserDivide()
    {
        Tuple<GameObject, Vector3,string,Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(positionLaser, LaserObject.transform.forward, layerTriangle, inputLine.material);

        triangle = objects.Item1;
        Vector3 _position = objects.Item2;
        string _name = objects.Item3;
        Material _mat = objects.Item4;

        inputLine.SetPosition(0, positionLaser);
        inputLine.SetPosition(1, _position);

        if (triangle != null)
        {
            if (RotationSpeed())
            {
                rotationSpeed = 5;
            }
            else
            {
                rotationSpeed = -5;
            }
            triangle.GetComponentInParent<TriangleScript>().CheckPlane(_position, _name, true, _mat, this.gameObject);
        }
        laserReset("Divide");
    }

    void LaserStart()
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(positionLaser, LaserObject.transform.forward, LayerStart, inputLine.material);

        Vector3 _position = objects.Item2;

        inputLine.SetPosition(0, positionLaser);
        inputLine.SetPosition(1, _position);

        laserReset("all");

    }
    void LaserFinal()
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(positionLaser, LaserObject.transform.forward, LayerFinal, inputLine.material);

        laserFinal = objects.Item1;
        Vector3 _position = objects.Item2;
        Vector3 _posDir = objects.Item5;

        inputLine.SetPosition(0, positionLaser);
        inputLine.SetPosition(1, _position);

        if (laserFinal != null)
        {
            if (RotationSpeed())
            {
                rotationSpeed = 5;
            }
            else
            {
                rotationSpeed = -5;
            }
            laserFinal.GetComponent<CheckLaser>().ReceivedLaser(true, inputLine.material);
        }
        laserReset("Final");
    }



    void LaserWall()
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(positionLaser, LaserObject.transform.forward, layerWalls, inputLine.material);

        Vector3 _position = objects.Item2;

        inputLine.SetPosition(0, positionLaser);
        inputLine.SetPosition(1, _position);

        laserReset("all");

        if (RotationSpeed())
        {
            rotationSpeed = 15;
        }
        else
        {
            rotationSpeed = -15;
        }
    }

    void LaserDraw()
    {
        switch (raycastLine.SearchLaser(positionLaser, LaserObject.transform.forward, this.gameObject))
            {
                case 6:
                    LaserMirror();
                    break;
                case 7:
                    LaserColor();
                    break;
                case 8:
                    LaserDivide();
                    break;
                case 9:
                    LaserWall();
                    break;
                case 10:
                    LaserStart();
                    break;
                case 11:
                    LaserFinal();
                    break;
            }

    }


    
    void laserReset(string _name)
    {
        switch(_name)
        {
            case "Mirror":
                if (cubeColor != null)
                    cubeColor.GetComponentInParent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material, null);
                triangle = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material);
                laserFinal = null;
                break;

            case "Color":
                if (reflexive != null)
                    reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material,transform.position, null);
                reflexive = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material, null);
                triangle = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material);
                laserFinal = null;
                break;

            case "Divide":
                if (reflexive != null)
                    reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material, transform.position, null);
                reflexive = null;

                if (cubeColor != null)
                    cubeColor.GetComponentInParent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material);
                laserFinal = null;
                break;

            case "Final":
                if (reflexive != null)
                    reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material, transform.position, null);
                reflexive = null;

                if (cubeColor != null)
                    cubeColor.GetComponentInParent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material, null);
                triangle = null;
                break;

            case "all":
                if (reflexive != null)
                    reflexive.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material, transform.position, null);
                reflexive = null;

                if (cubeColor != null)
                    cubeColor.GetComponentInParent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material, null);
                triangle = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material);
                laserFinal = null;
                break;
        }
    }

}
