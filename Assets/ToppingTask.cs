using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ToppingTask : MonoBehaviour, IPointerClickHandler
{

    decimal numUp, numDown;

    public TextMeshProUGUI textUp,textDown;

    ObjectManager objectManager;

    private void OnEnable()
    {
        numUp = 0.0m;
        numDown = 0.0m;

        objectManager = FindObjectOfType<ObjectManager>();
    }

    private void OnDisable()
    {
        Objects obj = objectManager.FindStateOfObject(ObjectState.Toppings);

        obj.numUp = float.Parse(numUp.ToString());
        obj.numDown = float.Parse(numDown.ToString());

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        Image clickedButton = clickedObject.GetComponent<Image>();
        if (clickedButton != null)
        {
            // Acciones a realizar cuando se hace clic en un botón
            if (clickedButton.name == "LeftArrowDown")
            {
                numDown -= 0.1m;
                if (numDown < 0.0m)
                {
                    numDown = 0.0m;
                }
                textDown.text = numDown.ToString();
            }
            else if (clickedButton.name == "RightArrowDown")
            {
                numDown += 0.1m;
                textDown.text = numDown.ToString();
            }
            else if (clickedButton.name == "RightArrowUp")
            {
                numUp += 0.1m;
                textUp.text = numUp.ToString();
            }
            else if (clickedButton.name == "LeftArrowUp")
            {
                numUp -= 0.1m;
                if (numUp < 0.0m)
                {
                    numUp = 0.0m;
                }
                textUp.text = numUp.ToString();
            }
        }

    }
}
