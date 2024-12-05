using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireTask : MonoBehaviour
{
    public List<Color> _wireColor = new List<Color>();
    public List<Wire> _leftWire = new List<Wire>();
    public List<Wire> _rightWire = new List<Wire>();

    public Wire CurrentDraggedWire;
    public Wire CurrentHoveredWire;

    public bool isTaskCompleted = false;

    private List<Color> _availableColor;
    private List<int> _availableLeftWireIndex;
    private List<int> _availableRightWireIndex;

    ObjectManager objectManager;

    private void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();

        _availableColor = new List<Color>(_wireColor);
        _availableLeftWireIndex = new List<int>();
        _availableRightWireIndex = new List<int>();

        for (int i = 0; i < _leftWire.Count; i++)
        {
            _availableLeftWireIndex.Add(i);
        }
        for (int i = 0; i < _rightWire.Count; i++)
        {
            _availableRightWireIndex.Add(i);
        }
        // Color picking
        while (_availableColor.Count > 0 && _availableLeftWireIndex.Count > 0 &&  _availableRightWireIndex.Count > 0)
        {

            Color pickedColor = _availableColor[Random.Range(0, _availableColor.Count)];

            int pickedLeftWireIndex = Random.Range(0, _availableLeftWireIndex.Count);
            int pickedRightWireIndex = Random.Range(0, _availableRightWireIndex.Count);

            _leftWire[_availableLeftWireIndex[pickedLeftWireIndex]].SetColor(pickedColor);
            _rightWire[_availableRightWireIndex[pickedRightWireIndex]] .SetColor(pickedColor);

            _availableColor.Remove(pickedColor);
            _availableLeftWireIndex.RemoveAt(pickedLeftWireIndex);
            _availableRightWireIndex.RemoveAt(pickedRightWireIndex);
        }

        //StartCoroutine(CheckTaskCompleted());
    }

    public void CheckTaskCompleted()
    {
            int successfulWires = 0;

            for (int i = 0; i < _rightWire.Count; i++)
            {
                if (_rightWire[i].isSucces) { successfulWires++; }
            }

            if (successfulWires >= _rightWire.Count)
            {
                Objects obj = objectManager.FindStateOfObject(ObjectState.Cables);

                obj.cablesCheck = true;
                for (int i = 0; i < _rightWire.Count; i++)
                {
                    _rightWire[i].isSucces = false;
                }
                GameManager.Instance.State = GameState.Playing;


            }
            else
            {
                Debug.Log("Task Incompleted");
            }
    }
}
