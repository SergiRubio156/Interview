using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RobotAnimationCode : MonoBehaviour
{
    // borrar
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("robot_percha_taller");
    }
    // borrar
    private GameObject go;
    public void UnParentRobot()
    {
        foreach(Transform g in this.gameObject.GetComponentsInChildren<Transform>())
        {
            if(g.gameObject.tag == "RobotAnimacion")
            {
                go = g.gameObject;
                break;
            }
        }
        go.gameObject.transform.parent = null;
    }
}
