using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLaser : MonoBehaviour
{
    public GameObject finalCheck;
    public Material mat;

    float velocidadRotacion = 5.0f;
    // Start is called before the first frame update
    void Awake()
    {
        finalCheck = this.gameObject.transform.parent.gameObject;
    }

    public void ReceivedLaser(bool _bool,Material _mat)
    {
        if (_bool)
        {
            if(ConfirmColor(_mat.color))
                finalCheck.GetComponent<FinalCheck>().CheckList(_bool, gameObject.name);
        }
        else if(!_bool)
        {
            finalCheck.GetComponent<FinalCheck>().CheckList(_bool, gameObject.name);
        }
    }

    bool ConfirmColor(Color _color)
    {
        if (mat.color == _color)
        {
            return true;
        }
        return false;
    }

}
