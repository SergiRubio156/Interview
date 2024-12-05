using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineObject : MonoBehaviour
{
   private Material outiLineMaterial;

    // Start is called before the first frame update
    void Start()
    {
        outiLineMaterial = GetComponentInChildren<MeshRenderer>().material;
        outiLineMaterial.SetColor("_Outline_Color", Color.white);
    }
}
