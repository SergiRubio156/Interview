using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Objects : MonoBehaviour
{
    public int id;
    public GameObject name;
    public ObjectState state;
    BoxCollider boxCollider;
    Rigidbody rb;
    private void Start()
    {
        name = this.gameObject;
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

}
