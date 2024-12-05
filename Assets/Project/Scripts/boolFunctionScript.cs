using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boolFunctionScript : MonoBehaviour
{
    bool end;
    bool AlwaysReturnTrue()
        // no hace falta ; por que le sigue una funcion)
    {
        if(end == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(AlwaysReturnTrue());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
