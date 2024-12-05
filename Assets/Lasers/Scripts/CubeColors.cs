using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CubeColors : MonoBehaviour
{
    public bool red, blue, yellow;

    int layerWalls, layerMirror, layerCylinder, layerTriangle, LayerStart, LayerFinal;

    RaycastHit hit;
    public GameObject sun;
    LineRenderer inputLine;
    Vector3 reflectiveRayPoint;

    public Material[] nameColor = new Material[4];
    public MeshRenderer[] renderer = new MeshRenderer[3];
    public GameObject laserSun;
    bool checkMirror;

    public GameObject cubeColor = null;
    public GameObject reflexiveCube = null;
    public GameObject triangle = null;
    public GameObject laserFinal = null;

    public RaycastLine raycastLine;

    // Start is called before the first frame update
    void Start()
    {
        inputLine = laserSun.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        layerMirror = 1 << 6;
        layerCylinder = 1 << 7;
        layerTriangle = 1 << 8;
        layerWalls = 1 << 9;
        LayerStart = 1 << 10;
        LayerFinal = 1 << 11;

        if (ConfirmLine())
        {
            laserReset("all");
        }
    }

    public bool ConfirmLine()
    {
        if (inputLine.GetPosition(0) == Vector3.zero)
            return true;
        return false;
    }

    void LaserMirror()
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(laserSun.transform.position, laserSun.transform.forward, layerMirror, inputLine.material);

        if (reflexiveCube != null)
        {
            if (reflexiveCube != objects.Item1)
            {
                reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material, transform.position, null);
                reflexiveCube = null;
            }
        }

        reflexiveCube = objects.Item1;
        Vector3 _position = objects.Item2;
        string _name = objects.Item3;
        Material _mat = objects.Item4;
        Vector3 _posDir = objects.Item5;

        Vector3 reflectiveRayPoint = Vector3.Reflect(_position - transform.position, _posDir);

        inputLine.SetPosition(0, laserSun.transform.position);
        inputLine.SetPosition(1, _position);



        if (reflexiveCube != null)
            reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(_position, reflectiveRayPoint, true, _mat, transform.position, this.gameObject);
        laserReset("Mirror");
    }

    void LaserDivide()
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(laserSun.transform.position, laserSun.transform.forward, layerTriangle, inputLine.material);

        triangle = objects.Item1;
        Vector3 _position = objects.Item2;
        string _name = objects.Item3;
        Material _mat = objects.Item4;

        inputLine.SetPosition(0, laserSun.transform.position);
        inputLine.SetPosition(1, _position);

        if (triangle != null)
            triangle.GetComponentInParent<TriangleScript>().CheckPlane(_position, _name, true, _mat, this.gameObject);

        laserReset("Divide");

    }

    void LaserStart()
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(laserSun.transform.position, laserSun.transform.forward, LayerStart, inputLine.material);

        GameObject obj = objects.Item1;
        Vector3 _position = objects.Item2;

        inputLine.SetPosition(0, laserSun.transform.position);
        inputLine.SetPosition(1, _position);

        laserReset("all");

    }

    void LaserWall()
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(laserSun.transform.position, laserSun.transform.forward, layerWalls, inputLine.material);

        Vector3 _position = objects.Item2;

        inputLine.SetPosition(0, laserSun.transform.position);
        inputLine.SetPosition(1, _position);

        laserReset("all");
    }
    void LaserFinal()
    {
        Tuple<GameObject, Vector3, string, Material, Vector3> objects = raycastLine.GetGameObjectAndPosition(laserSun.transform.position, laserSun.transform.forward, LayerFinal, inputLine.material);

        laserFinal = objects.Item1;
        Vector3 _position = objects.Item2;
        Vector3 _posDir = objects.Item5;

        inputLine.SetPosition(0, laserSun.transform.position);
        inputLine.SetPosition(1, _position);

        if (laserFinal != null)
            laserFinal.GetComponent<CheckLaser>().ReceivedLaser(true, inputLine.material);
        laserReset("Final");

    }
    public void RecivedColors(Material _mat, bool _bool,string _name)
    {
        Color _color = _mat.color;
        if (!red || !blue || !yellow || !_bool)
        {
            for (int i = 0; i < nameColor.Length; i++)
            {
                if (nameColor[i].color == _color)
                {
                    switch (i)
                    {
                        case 0:
                            red = _bool;
                            RenderColor(_name, _color, _bool,"Red");
                            break;
                        case 1:
                            blue = _bool;
                            RenderColor(_name, _color, _bool,"Blue");
                            break;
                        case 2:
                            yellow = _bool;
                            RenderColor(_name, _color, _bool,"Yellow");
                            break;
                    }
                }
            }
            if (renderer[3].material != nameColor[3])
                RenderColor("5", _color, false, "Sun");
        }
        if (red && blue && yellow)
        {
            RenderColor("4", _color, _bool,"Yellow");
            LaserDraw();
        }
        else
        {
            inputLine.SetPosition(0, Vector3.zero);
            inputLine.SetPosition(1, Vector3.zero);
        }

    }

    void RenderColor(string _name,Color _color,bool _bool,string _name2)
    {
        switch (_name)
        {
            case "1":
                if (_bool)
                    renderer[0].material.color = _color;
                break;
            case "2":
                if (_bool)
                    renderer[1].material.color = _color;
                break;
            case "3":
                if(_bool)
                    renderer[2].material.color = _color;
                break;
            case "4":
                if(_bool)
                    renderer[3].material = nameColor[4];
                break;
            default:
                if (_name2 == "Red")
                    forColor(_color);
                if (_name2 == "Blue")
                    forColor(_color);
                if (_name2 == "Yellow")
                    forColor(_color);
                if (_name2 == "Sun")
                    renderer[3].material = nameColor[3];
                break;
        }
    }

    void forColor(Color _name)
    {
        for(int i = 0; i < renderer.Length; i++)
        {
            if(renderer[i].material.color == _name)
                renderer[i].material = nameColor[3];
        }
    }
    void LaserDraw()
    {
        switch (raycastLine.SearchLaser(laserSun.transform.position, laserSun.transform.forward, this.gameObject))
        {
            case 6: //MIRROR
                LaserMirror();
                break;
            case 8: //TRIANGLE
                LaserDivide();
                break;
            case 9: //WALL
                LaserWall();
                break;
            case 10: //LaserStart
                LaserStart();
                break;
            case 11: //LaserFinal
                LaserFinal();
                break;
            default:
                break;
        }
    }

    void laserReset(string _name)
    {
        switch (_name)
        {
            case "Mirror":
                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material, false,_name);
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
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material, Vector3.zero,null);
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
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material, Vector3.zero,null);
                reflexiveCube = null;

                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;

                if (laserFinal != null)
                    laserFinal.GetComponent<CheckLaser>().ReceivedLaser(false, inputLine.material);
                laserFinal = null;
                break;

            case "Final":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material, transform.position,null);
                reflexiveCube = null;

                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material, false, _name);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, _name, false, inputLine.material, null);
                triangle = null;
                break;

            case "all":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material, Vector3.zero,null);
                reflexiveCube = null;

                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material, false, _name);
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
