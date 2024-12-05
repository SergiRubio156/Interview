using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
 

public enum ObjectState
{
    NoTaked,
    Taked,
    Toppings,
    Cables,
    Color,
    Arm,
    Cinta
};

public class ObjectManager : MonoBehaviour
{
    [SerializeField] 
    private static List<Objects> objectList = new List<Objects>();

    public static int totalId = 1;

    ObjectState State = ObjectState.NoTaked;

    // Start is called before the first frame update
    void Start()
    {
        RecivedRobotsList();
    }
    public void RecivedRobotsList()
    {
        Objects[] components = FindObjectsOfType<Objects>();

        for (int i = 0; i < components.Length; i++)
        {
            Debug.Log(components[i].name);
            // Verificar si ya existe un componente con el mismo nombre en el array RobotCards
            if (!objectList.Any(card => card.name == components[i].name))
            {
                // Si no existe, agrega el componente al array RobotCards
                objectList.Add(components[i]);
                objectList[i].id = totalId;
                totalId++;
            }
        }
    }

    public void RemoveRobotsList(Objects _obj)
    {
        foreach(Objects obj in objectList)
        {
            if(obj == _obj)
            {
                objectList.Remove(obj);
                break;
            }
        }
    }

    public int GetObjectPositionInList(GameObject _obj)
    {
        if (_obj.GetComponent<Objects>() != null)
        {
            for (int i = 0; i < objectList.Count; i++)
            {
                if (objectList[i].name == _obj.GetComponent<Objects>().name)
                    return i;
            }
        }
        return -1;
    }

    public ObjectState GetObjectStateInList(int _id)
    {

        return objectList[_id].state;
    }

    public Objects FindStateOfObject(ObjectState _state)
    {
        foreach(Objects _obj in objectList)
        {
            if (_obj.state == _state)
                return _obj;
        }
        return null;
    }
    public bool FindBoolStateOfObject(ObjectState _state)
    {
        foreach (Objects _obj in objectList)
        {
            if (_obj.state == _state)
                return true;
        }
        return false;
    }
    public bool GetObjectBoolInList(int i , string _name)
    {
        switch (_name)
        {
            case "Color":
                return objectList[i].colorCheck;

            case "Toppings":
                return objectList[i].toppingCheck;

            case "Wire":
                return objectList[i].cablesCheck;

            default:
                return true;
        }

    }

    public void ObjectGameState(int _index, ObjectState newState)
    {
        State = objectList[_index].state;
        objectList[_index].state = newState;

        if (State != newState)
        {
            switch (newState)
            {
                case ObjectState.NoTaked:
                    ObjectNoTaked();
                    State = newState;
                    break;

                case ObjectState.Taked:
                    ObjectTaked();
                    State = newState;
                    break;

                case ObjectState.Toppings:
                    ObjectToppings();
                    State = newState;
                    break;

                case ObjectState.Cables:
                    ObjectCables();
                    State = newState;
                    break;

                case ObjectState.Color:
                    ObjectColors();
                    State = newState;
                    break;
                case ObjectState.Cinta:
                    ObjectCinta();
                    State = newState;
                    break;
                case ObjectState.Arm:
                    ObjectArm();
                    State = newState;
                    break;
                default:
                    Debug.Log("Error");
                    break;
                   
            }
        }
    }

    public virtual void ObjectNoTaked()
    {
    }

    public virtual void ObjectTaked()
    {
        
    }

    public virtual void ObjectToppings()
    {
    }

    public virtual void ObjectCables()
    {
       
    }
    public virtual void ObjectArm()
    {

    }
    public virtual void ObjectColors()
    {
    }

    public virtual void ObjectCinta()
    {
    }


}
