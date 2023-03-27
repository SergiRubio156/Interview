using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflexiveRay : MonoBehaviour
{
    RaycastHit hit;
    public LineRenderer inputLine;
    int layerWalls, layerMirror, layerCylinder, layerTriangle, LayerStart;

    public GameObject cubeColor;
    public GameObject reflexiveCube;
    public GameObject triangle;

    Vector3 reflectiveRayPoint;
    Vector3 point;
    Vector3 transformStart;
    bool checkBool1;
    bool checkBool;
    // Start is called before the first frame update
    void Start()
    {
        inputLine = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        layerMirror = 1 << 6;
        layerCylinder = 1 << 7;
        layerTriangle = 1 << 8;
        layerWalls = 1 << 9;
        LayerStart = 1 << 10;

        LaserDraw();
    }

    string SearchLaser()
    {
        if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, 100))
        {
            return hit.transform.gameObject.name;
        }

        return "null";
    }
    void LaserMirror()
    {
        if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, 100, layerMirror))
        {

            Vector3 _hitPoint = hit.point;
            Vector3 reflectiveRayPoint2 = Vector3.Reflect(hit.point - point, hit.normal);


            inputLine.SetPosition(0, point);
            inputLine.SetPosition(1, hit.point);

            reflexiveCube = hit.transform.gameObject;
            hit.transform.gameObject.GetComponent<ReflexiveRay>().ReflexiveMirror(hit.point, reflectiveRayPoint2, true, inputLine.material.color,transform.position);

            laserReset("Mirror");
        }
    }

    void LaserColor()
    {
        if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, 100, layerCylinder))
        {
            inputLine.SetPosition(0, point);
            inputLine.SetPosition(1, hit.point);


            cubeColor = hit.transform.gameObject;
            hit.transform.gameObject.GetComponent<CubeColors>().RecivedColors(inputLine.material.color, true);

            laserReset("Color");
        }
    }

    void LaserDivide()
    {
        if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, 100, layerTriangle))
        {
            inputLine.SetPosition(0, point);
            inputLine.SetPosition(1, hit.point);


            triangle = hit.transform.gameObject;
            hit.transform.gameObject.GetComponentInParent<TriangleScript>().CheckPlane(hit.point, hit.transform.gameObject.name, true, inputLine.material.color);

            laserReset("Divide");
        }

    }

    void  LaserWall()
    {
        if (Physics.Raycast(point, reflectiveRayPoint * 3 - point, out hit, 100, layerWalls))
        {
            inputLine.SetPosition(0, point);
            inputLine.SetPosition(1, hit.point);

            laserReset("all");

        }

    }
    bool LaserConfirm()
    {
        Debug.DrawRay(point, transformStart - point, Color.black);
        if (Physics.Raycast(point, transformStart - point, out hit, 100))
        {

            if (hit.transform.gameObject.name == "CubeYellow" || hit.transform.gameObject.name == "CubeRed" || hit.transform.gameObject.name == "CubeBlue")
            {
                return true;
            }
        }
        return false;
    }

    bool ReflexConfirm()
    {
        Debug.DrawRay(point, transformStart - point, Color.black);
        if (Physics.Raycast(point, transformStart - point, out hit, 100))
        {

            if (hit.transform.gameObject.name == "Mirror")
            {
                return true;
            }
        }
        return false;
    }


    void LaserDraw()
    {
        if ((LaserConfirm() && checkBool) || (ReflexConfirm() && checkBool1))
        {
            switch (SearchLaser())
            {
                case "Mirror":
                    LaserMirror();
                    break;
                case "Cylinder":
                    LaserColor();
                    break;
                case "1":
                    LaserDivide();
                    break;
                case "2":
                    LaserDivide();
                    break;
                case "3":
                    LaserDivide();
                    break;
                case "Wall":
                    LaserWall();
                    break;
                default:
                    checkBool = false;
                    break;
            }
        }
        else
        {
            inputLine.SetPosition(0, Vector3.zero);
            inputLine.SetPosition(1, Vector3.zero);
        }

    }


    public void ReceiveImpactPoint(Vector3 _point,Vector3 _reflectiveRayPoint, bool _bool,Color _color, Vector3 _transformStart)
    {
        point = _point;
        reflectiveRayPoint = _reflectiveRayPoint;
        checkBool = _bool;
        inputLine.material.color = _color;
        transformStart = _transformStart;

        LaserDraw();
    }

    public void ReflexiveMirror(Vector3 _point, Vector3 _reflectiveRayPoint, bool _bool, Color _color, Vector3 _transformStart)
    {
        point = _point;
        reflectiveRayPoint = _reflectiveRayPoint;
        checkBool1 = _bool;
        inputLine.material.color = _color;
        transformStart = _transformStart;

        LaserDraw();
    }



    void laserReset(string _name)
    {
        switch (_name)
        {
            case "Mirror":
                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.color, false);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle.name, false, inputLine.material.color);
                triangle = null;

                break;
            case "Color":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, Vector3.zero);
                reflexiveCube = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle.name, false, inputLine.material.color);
                triangle = null;

                break;
            case "Divide":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, Vector3.zero);
                reflexiveCube = null;

                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.color, false);
                cubeColor = null;

                break;
            case "all":
                if (reflexiveCube != null)
                    reflexiveCube.GetComponent<ReflexiveRay>().ReceiveImpactPoint(Vector3.zero, Vector3.zero, false, inputLine.material.color, Vector3.zero);
                reflexiveCube = null;

                if (cubeColor != null)
                    cubeColor.GetComponent<CubeColors>().RecivedColors(inputLine.material.color, false);
                cubeColor = null;

                if (triangle != null)
                    triangle.GetComponentInParent<TriangleScript>().CheckPlane(Vector3.zero, triangle.name, false, inputLine.material.color);
                triangle = null;
                break;
        }
    }
}
