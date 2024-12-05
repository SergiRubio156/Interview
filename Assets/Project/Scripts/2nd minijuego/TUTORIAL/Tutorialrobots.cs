using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorialrobots : MonoBehaviour
{
    public bool tutorialOn = true;
    public List<GameObject> objetosTutorial = new List<GameObject>();
    int paso = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!tutorialOn) { return; }
        //objetosTutorial[paso].GetComponentInChildren<MeshRenderer>().material
    }
}
