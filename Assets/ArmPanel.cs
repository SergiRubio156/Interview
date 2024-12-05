using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArmPanel : MonoBehaviour, IPointerClickHandler
{
    ObjectManager objectManager;
    Objects obj;
    public AnimationArm animationArm;
    public Transform positionCube;
    [SerializeField]
    public GameObject parentObject; // GameObject padre que contiene el componente Horizontal Layout Group
    int brazoRobot;

    private void OnEnable()
    {
        objectManager = FindObjectOfType<ObjectManager>();

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        Image clickedButton = clickedObject.GetComponent<Image>();

        if (clickedButton != null)
        {
            if (clickedButton.name == "Image1")
            {
                brazoRobot = 1;
                animationArm.AnimationRun(brazoRobot);

                GameManager.Instance.State = GameState.Playing;

            }
            else if (clickedButton.name == "Image2")
            {

                brazoRobot = 2;
                animationArm.AnimationRun(brazoRobot);

                GameManager.Instance.State = GameState.Playing;


            }
            else if (clickedButton.name == "Image3")
            {

                brazoRobot = 3;

                animationArm.AnimationRun(brazoRobot);

                GameManager.Instance.State = GameState.Playing;


            }
        }

    }
}
