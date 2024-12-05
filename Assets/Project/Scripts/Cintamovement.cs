using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cintamovement : MonoBehaviour
{

    public float velocidad;
    public Transform cintaFinalPos;
    public Transform cintaPos;

    ObjectManager objectManager;
    Objects obj;
    bool moveBool;
    // Start is called before the first frame update
    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(moveBool)
        {
     
            if (obj != null)
            {
                Vector3 posicionActual = obj.obj.transform.position;
                Vector3 posicionObjetivo = cintaFinalPos.position;

                Vector3 nuevaPosicion = Vector3.MoveTowards(posicionActual, posicionObjetivo, velocidad * Time.deltaTime);

                // Asignar la nueva posición al objeto a controlar
                obj.transform.position = nuevaPosicion;
            }
            else
            {
                moveBool = false;
            }
        }
    }
    
    public void BoolCinta()
    {
        moveBool = true;
        obj = objectManager.FindStateOfObject(ObjectState.Cinta);
    }

}
