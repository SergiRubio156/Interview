using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RaycastLine : MonoBehaviour
{
    RaycastHit hit;
    public int SearchLaser(Vector3 point, Vector3 Dir)
    {
        Ray ray = new Ray(point, Dir);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit.transform.gameObject.layer;
        }

        return 0;
    }

    public Tuple<GameObject, Vector3,string,Material> GetGameObjectAndPosition(Vector3 _point, Vector3 _Dir, int layer, Material mat)
    {
        Ray ray = new Ray(_point, _Dir);

        GameObject obj = null;
        string _name = "";
        Material _mat = mat;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            obj = hit.transform.gameObject;
            _name = hit.transform.gameObject.name;
            _mat = mat;

            return Tuple.Create(obj, hit.point,_name, _mat);
        }

        return Tuple.Create(obj, Vector3.zero, _name, _mat);
    }
}
