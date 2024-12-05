using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RaycastLine : MonoBehaviour
{
    RaycastHit hit;

    public int SearchLaser(Vector3 point, Vector3 Dir , GameObject _obj)
    {
        Ray ray = new Ray(point, Dir);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(_obj != hit.transform.gameObject)
                return hit.transform.gameObject.layer;
        }

        return 0;
    }

    public Tuple<GameObject, Vector3,string,Material,Vector3> GetGameObjectAndPosition(Vector3 _point, Vector3 _Dir, int layer, Material mat)
    {
        Ray ray = new Ray(_point, _Dir);

        GameObject obj = null;
        string _name = "";
        Material _mat = mat;
        Debug.DrawRay(_point, _Dir * 100, Color.blue);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            obj = hit.transform.gameObject;
            _name = hit.transform.gameObject.name;
            _mat = mat;
            Vector3 _pos = hit.point;
            Vector3 _posDir = hit.normal;

            return Tuple.Create(obj, _pos, _name, _mat,_posDir);
        }

        return Tuple.Create(obj, Vector3.zero, _name, _mat, Vector3.zero);
    }
}
