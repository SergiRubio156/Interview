using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriangleScript : MonoBehaviour
{
    public List<Vector3> vectorRecibed = new List<Vector3>()
    {
    new Vector3(0, 0, 0),
    new Vector3(0, 0, 0),
    new Vector3(0, 0, 0),
    };
     public GameObject[] Sides = new GameObject[3];

     GameObject[] reflexive = new GameObject[3];
     GameObject[] cubeColor = new GameObject[3];
     GameObject[] triangle = new GameObject[3];
     GameObject[] laserFinal = new GameObject[3];

    LineRenderer[] inputLine = new LineRenderer[3];

    public GameObject LaserObject = null;
    public Material mat;

    Vector3 positionLaser;
    public GameObject objectRecvied;
    public RaycastLine raycastLine;

    RaycastHit hit;


    int layerWalls, layerMirror, layerCylinder, layerTriangle, LayerStart, LayerFinal;

    public Vector3 point;
    public int nameSide = 0;
    public bool checkBool;
    Renderer renderer = new Renderer();

    // Start is called before the first frame update
    void Start()
    {
        renderer = LaserObject.GetComponent<Renderer>();
        positionLaser = LaserObject.transform.position;
        for (int i = 0; i < vectorRecibed.Count; i++)
        {
            reflexive[i] = null;
            cubeColor[i] = null;
            triangle[i] = null;
            laserFinal[i] = null;
            inputLine[i] = Sides[i].GetComponent<LineRenderer>();
        }
        Render();
    }

    // Update is called once per frame
    void Update()
    {
        layerMirror = 1 << 6;
        layerCylinder = 1 << 7;
        layerTriangle = 1 << 8;
        layerWalls = 1 << 9;
        LayerStart = 1 << 10;
        LayerFinal = 1 << 11;

        Render();
        if (ConfirmLine(nameSide) && !checkBool)
        {
            for (int i = 0; i < inputLine.Length; i++)
            {
                laserReset("all", i);
            }
        }
    }

    public bool ConfirmLine(int _name)
    {
        if (inputLine[nameSide].GetPosition(1) == Vector3.zero)
            return true;
        return false;
    }

    void Render()
    {
        positionLaser = LaserObject.transform.position;
        for (int i = 0; i < vectorRecibed.Count; i++)
        {
            vectorRecibed[i] = Sides[i].transform.localToWorldMatrix.MultiplyVector(Vector3.forward).normalized;
        }
    }

    void LaserMirror(int i)
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(Sides[i].transform.position, vectorRecibed[i], layerMirror, inputLine[i].material);

        if (reflexive[i] != null)
        {
            if (reflexive[i] != objects.Item1)
            {
                reflexive[i].GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine[i].material, transform.position, null);
                reflexive[i] = null;
            }
        }

        reflexive[i] = objects.Item1;
        Vector3 _position = objects.Item2;
        string _name = objects.Item3;
        Material _mat = objects.Item4;
        Vector3 _posDir = objects.Item5;

        Vector3 reflectiveRayPoint = Vector3.Reflect(_position - transform.position, _posDir);

        inputLine[i].SetPosition(0, positionLaser);
        inputLine[i].SetPosition(1, _position);



        if (reflexive[i] != null)
            reflexive[i].GetComponent<ReflexiveRay>().ReceiveImpactPoint(_position, reflectiveRayPoint, true, _mat, transform.position, this.gameObject);
        laserReset("Mirror",i);
    }

    void LaserColor(int i)
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(Sides[i].transform.position, vectorRecibed[i], layerCylinder, inputLine[i].material);

        string _name = objects.Item3;

        if (cubeColor[i] != null)
        {
            if (cubeColor[i] != objects.Item1)
            {
                cubeColor[i].GetComponentInParent<CubeColors>().RecivedColors(inputLine[i].material, false, _name);
                cubeColor[i] = null;
            }
        }

        cubeColor[i] = objects.Item1;
        Vector3 _position = objects.Item2;
        Material _mat = objects.Item4;

        inputLine[i].SetPosition(0, positionLaser);
        inputLine[i].SetPosition(1, _position);

        if (cubeColor[i] != null)
            cubeColor[i].GetComponentInParent<CubeColors>().RecivedColors(_mat, true, _name);

        laserReset("Color",i);
    }

    void LaserDivide(int i)
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(Sides[i].transform.position, vectorRecibed[i], layerTriangle, inputLine[i].material);

        string _name = objects.Item3;

        if (triangle[i] != null)
        {
            if (triangle[i] != objects.Item1)
            {
                triangle[i].GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine[i].material, null);
                triangle[i] = null;
            }
        }

        triangle[i] = objects.Item1;
        Vector3 _position = objects.Item2;
        Material _mat = objects.Item4;

        inputLine[i].SetPosition(0, positionLaser);
        inputLine[i].SetPosition(1, _position);

        if (triangle[i] != null)
            triangle[i].GetComponentInParent<TriangleScript>().CheckPlane(_position, _name, true, _mat, this.gameObject);

        laserReset("Divide", i);
        
    }

    void LaserStart(int i)
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(Sides[i].transform.position, vectorRecibed[i], LayerStart, inputLine[i].material);

        Vector3 _position = objects.Item2;

        inputLine[i].SetPosition(0, positionLaser);
        inputLine[i].SetPosition(1, _position);

        laserReset("all", i);
    }

    void LaserWall(int i)
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(Sides[i].transform.position, vectorRecibed[i], layerWalls, inputLine[i].material);

        Vector3 _position = objects.Item2;

        inputLine[i].SetPosition(0, positionLaser);
        inputLine[i].SetPosition(1, _position);

        laserReset("all",i);
    }
    void LaserFinal(int i)
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(Sides[i].transform.position, vectorRecibed[i], LayerFinal, inputLine[i].material);

        laserFinal[i] = objects.Item1;
        Vector3 _position = objects.Item2;
        Vector3 _posDir = objects.Item5;

        inputLine[i].SetPosition(0, positionLaser);
        inputLine[i].SetPosition(1, _position);

        if (laserFinal != null)
            laserFinal[i].GetComponent<CheckLaser>().ReceivedLaser(true, inputLine[i].material);
        laserReset("Final" , i);
    }
    void LaserDraw(int _name)
    {
        if (checkBool)
        {
            for (int i = 0; i < inputLine.Length; i++)
            {
                if (i != _name)
                {
                    switch (raycastLine.SearchLaser(Sides[i].transform.position, vectorRecibed[i], this.gameObject))
                    {
                        case 6:
                            LaserMirror(i);
                            break;
                        case 7:
                            LaserColor(i);
                            break;
                        case 8:
                            LaserDivide(i);
                            break;
                        case 9:
                            LaserWall(i);
                            break;
                        case 10:
                            LaserStart(i);
                            break;
                        case 11:
                            LaserFinal(i);
                            break;
                    }
                }
                else if (i == _name)
                {
                    inputLine[i].SetPosition(0, Vector3.zero);
                    inputLine[i].SetPosition(1, Vector3.zero);

                }
            }
        }
        else if (!checkBool)
        {
            renderer.material = mat;
            for (int i = 0; i < inputLine.Length; i++)
            {
                inputLine[i].SetPosition(0, Vector3.zero);
                inputLine[i].SetPosition(1, Vector3.zero);
            }

        }

    }

    public void CheckPlane(Vector3 _point, string _name, bool _bool,Material _mat, GameObject _gameObject)
    {
        if (_gameObject == objectRecvied || objectRecvied == null || _gameObject == null)
        {
            objectRecvied = _gameObject;
            point = _point;
            checkBool = _bool;

            if (_point == Vector3.zero)
            {
                checkBool = false;
            }

            if (_name == "1")
                nameSide = 0;
            else if (_name == "2")
                nameSide = 1;
            else if (_name == "3")
                nameSide = 2;

            renderer.material = _mat;         

            TakeColor(_mat);
            LaserDraw(nameSide);
        }
    }

    void TakeColor(Material _mat)
    {
        for (int i = 0; i < inputLine.Length; i++)
        {
            inputLine[i].material = _mat;
        }

    }
    void laserReset(string _name, int i)
    {
        switch (_name)
        {
            case "Mirror":
                if (cubeColor[i] != null)
                    cubeColor[i].GetComponentInParent<CubeColors>().RecivedColors(inputLine[i].material, false,_name);
                cubeColor[i] = null;

                if (triangle[i] != null)
                    triangle[i].GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle[i].name, false, inputLine[i].material, null);
                triangle[i] = null;

                if (laserFinal[i] != null)
                    laserFinal[i].GetComponent<CheckLaser>().ReceivedLaser(false, inputLine[i].material);
                laserFinal[i] = null;
                break;
            case "Color":
                if (reflexive[i] != null)
                    reflexive[i].GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine[i].material, transform.position, null);
                reflexive[i] = null;

                if (triangle[i] != null)
                    triangle[i].GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle[i].name, false, inputLine[i].material, null);
                triangle[i] = null;

                if (laserFinal[i] != null)
                    laserFinal[i].GetComponent<CheckLaser>().ReceivedLaser(false, inputLine[i].material);
                laserFinal[i] = null;
                break;
            case "Divide":
                if (reflexive[i] != null)
                    reflexive[i].GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine[i].material, transform.position, null);
                reflexive[i] = null;

                if (cubeColor[i] != null)
                    cubeColor[i].GetComponentInParent<CubeColors>().RecivedColors(inputLine[i].material, false,_name);
                cubeColor[i] = null;

                if (laserFinal[i] != null)
                    laserFinal[i].GetComponent<CheckLaser>().ReceivedLaser(false, inputLine[i].material);
                laserFinal[i] = null;
                break;
            case "Final":
                if (reflexive[i] != null)
                    reflexive[i].GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine[i].material, transform.position, null);
                reflexive[i] = null;

                if (cubeColor[i] != null)

                    cubeColor[i].GetComponentInParent<CubeColors>().RecivedColors(inputLine[i].material, false,_name);
                cubeColor[i] = null;

                if (triangle[i] != null)
                    triangle[i].GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle[i].name, false, inputLine[i].material, null);
                triangle[i] = null;
                break;
            case "all":
                if (reflexive[i] != null)
                    reflexive[i].GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine[i].material, transform.position, null);
                reflexive[i] = null;

                if (cubeColor[i] != null)

                    cubeColor[i].GetComponentInParent<CubeColors>().RecivedColors(inputLine[i].material, false,_name);
                cubeColor[i] = null;

                if (triangle[i] != null)
                    triangle[i].GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle[i].name, false, inputLine[i].material, null);
                triangle[i] = null;

                if (laserFinal[i] != null)
                    laserFinal[i].GetComponent<CheckLaser>().ReceivedLaser(false, inputLine[i].material);
                laserFinal[i] = null;
                break;
        }
    }
}