using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReflexiveRay : MonoBehaviour
{
    RaycastHit hit;
    public LineRenderer inputLine;

    int layerWalls, layerMirror, layerCylinder, layerTriangle, LayerStart, LayerFinal;

    public GameObject cubeColor = null;
    public GameObject reflexiveCube = null;
    public GameObject triangle = null;
    public GameObject laserFinal = null;

    public GameObject objectRecvied;

    Vector3 point;
    Vector3 forwardX = new Vector3();
    bool checkBool = false;
     
    //NECESARIOS
    public RaycastLine raycastLine;

    // Start is called before the first frame update
    void Start()
    {
        inputLine = GetComponentInChildren<LineRenderer>();
        forwardX = transform.TransformDirection(Vector3.right);

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
        forwardX = transform.TransformDirection(Vector3.right);

        if (ConfirmLine())
        {
            laserReset("all");
        }
    }

    public bool ConfirmLine()
    {
        if(inputLine.GetPosition(1) == Vector3.zero)
            return true;
        return false;
    }

    void LaserMirror()
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(point, forwardX, layerMirror, inputLine.material);

        if(objects.Item1 != this.gameObject)
            reflexiveCube = objects.Item1;
        Vector3 _position = objects.Item2;
        string _name = objects.Item3;
        Material _mat = objects.Item4;
        Vector3 _posDir = objects.Item5;

        Vector3 reflectiveRayPoint = Vector3.Reflect(_position - transform.position, _posDir); //hit.normal);

        inputLine.SetPosition(0, point);
        inputLine.SetPosition(1, _position);

        if (reflexiveCube != null)
            reflexiveCube.GetComponent<ReflexiveRay>().ReflexiveMirror(_position, reflectiveRayPoint, true, _mat, transform.position, this.gameObject);

        laserReset("Mirror");
    }


    void LaserColor()
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(point, forwardX, layerCylinder, inputLine.material);

        cubeColor = objects.Item1;
        Vector3 _position = objects.Item2;
        string _name = objects.Item3;
        Material _mat = objects.Item4;

        inputLine.SetPosition(0, point);
        inputLine.SetPosition(1, _position);

        if (cubeColor != null)
            cubeColor.GetComponentInParent<CubeColors>().RecivedColors(_mat, true, _name);

        laserReset("Color");
    }

    void LaserDivide()
    {
        Debug.Log("!");
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(point, forwardX, layerTriangle, inputLine.material);

        triangle = objects.Item1;
        Vector3 _position = objects.Item2;
        string _name = objects.Item3;
        Material _mat = objects.Item4;

        inputLine.SetPosition(0, point);
        inputLine.SetPosition(1, _position);

        if(triangle != null)
            triangle.GetComponentInParent<TriangleScript>().CheckPlane(_position, _name, true, _mat, this.gameObject);

        laserReset("Divide");

    }

    void  LaserWall()
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(point, forwardX, layerWalls, inputLine.material);

        Vector3 _position = objects.Item2;

        inputLine.SetPosition(0, point);
        inputLine.SetPosition(1, _position);

        laserReset("all");

        

    }
    void LaserStart()
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(point, forwardX, layerWalls, inputLine.material);

        Vector3 _position = objects.Item2;


        inputLine.SetPosition(0, point);
        inputLine.SetPosition(1, _position);

        laserReset("all");
    }
    void LaserFinal()
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(point, forwardX, LayerFinal, inputLine.material);

        laserFinal = objects.Item1;
        Vector3 _position = objects.Item2;
        Vector3 _posDir = objects.Item5;

        inputLine.SetPosition(0, point);
        inputLine.SetPosition(1, _position);

        if (laserFinal != null)
            laserFinal.GetComponent<CheckLaser>().ReceivedLaser(true, inputLine.material);
        laserReset("Final");

    }


    void LaserDraw()
    {

        if (checkBool)
        {

            switch (raycastLine.SearchLaser(point, forwardX, this.gameObject))
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
        else if(!checkBool)
        {
            inputLine.SetPosition(0, Vector3.zero);
            inputLine.SetPosition(1, Vector3.zero);
        }

    }


    public void ReceiveImpactPoint(Vector3 _point,Vector3 _reflectiveRayPoint, bool _bool,Material _mat, Vector3 _transformStart, GameObject _gameObject)
    {
        if(_gameObject == objectRecvied || objectRecvied == null || _gameObject == null)
        {
            objectRecvied = _gameObject;
            point = _point;
            checkBool = _bool;
            if (inputLine.material != _mat)
            {
                inputLine.material = _mat;
            }
            LaserDraw();
        }
        
    }



    public void ReflexiveMirror(Vector3 _point, Vector3 _reflectiveRayPoint, bool _bool, Material _mat, Vector3 _transformStart, GameObject _gameObject)
    {
        if (_gameObject == objectRecvied || objectRecvied == null || _gameObject == null)
        {
            objectRecvied = _gameObject;
            point = _point;
            checkBool = _bool;
            if (inputLine.material != _mat)
                inputLine.material = _mat;

            LaserDraw();
        }
    }



    void laserReset(string _name)
    {
        switch (_name)
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
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material, Vector3.zero, null);
                reflexiveCube = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material, null);
                triangle = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material);
                laserFinal = null;
                break;

            case "Divide":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material, Vector3.zero, null);
                reflexiveCube = null;
                objectRecvied = null;

                if (cubeColor != null)
                    cubeColor.GetComponentInParent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material);
                laserFinal = null;
                break;

            case "Final":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material, transform.position, null);
                reflexiveCube = null;

                if (cubeColor != null)
                    cubeColor.GetComponentInParent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material, null);
                triangle = null;
                break;

            case "all":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material, Vector3.zero, null);
                reflexiveCube = null;

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
