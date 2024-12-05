using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RobotPanel : MonoBehaviour, IPointerClickHandler
{
    ObjectManager objectManager;
    public Transform positionCube;
    [SerializeField]
    public GameObject[] prefabs = new GameObject[3];
    public GameObject parentObject; // GameObject padre que contiene el componente Horizontal Layout Group

    public TimeManager timeManager;
    private void OnEnable()
    {
        objectManager = FindObjectOfType<ObjectManager>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        timeManager.durationLvl1 = false;
        timeManager.durationLvl2 = true;
        timeManager.durationBetweenRobots = true;
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        Image clickedButton = clickedObject.GetComponent<Image>();

        if (clickedButton != null)
        {
            if (clickedButton.name == "Image1")
            {
                GameObject newImage = Instantiate(prefabs[0], parentObject.transform);
                newImage.transform.position = positionCube.transform.position;
                newImage.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                int existingImagesCount = parentObject.transform.childCount - 1; // Restar uno para excluir el propio objeto padre
                int newImageIndex = Mathf.Clamp(existingImagesCount + 1, 0, existingImagesCount);
                newImage.transform.SetSiblingIndex(newImageIndex);

                objectManager.RecivedRobotsList();

                Objects obj = newImage.GetComponent<Objects>();

                obj.robotUp = 1;
                newImage.name = "Robot " + ObjectManager.totalId;

                GameManager.Instance.State = GameState.Playing;
            }
            else if (clickedButton.name == "Image2")
            {
                GameObject newImage = Instantiate(prefabs[1], parentObject.transform);
                newImage.transform.position = positionCube.transform.position;
                newImage.transform.rotation = Quaternion.Euler(0f, 81.29f, 0f);

                int existingImagesCount = parentObject.transform.childCount - 1; // Restar uno para excluir el propio objeto padre
                int newImageIndex = Mathf.Clamp(existingImagesCount + 1, 0, existingImagesCount);
                newImage.transform.SetSiblingIndex(newImageIndex);

                objectManager.RecivedRobotsList();

                Objects obj = newImage.GetComponent<Objects>();

                obj.robotUp = 2;
                newImage.name = "Robot " + ObjectManager.totalId;

                GameManager.Instance.State = GameState.Playing;

            }
            else if (clickedButton.name == "Image3")
            {
                GameObject newImage = Instantiate(prefabs[2], parentObject.transform);
                newImage.transform.position = positionCube.transform.position;
                newImage.transform.rotation = Quaternion.Euler(0f, 81.29f, 0f);

                int existingImagesCount = parentObject.transform.childCount - 1; // Restar uno para excluir el propio objeto padre
                int newImageIndex = Mathf.Clamp(existingImagesCount + 1, 0, existingImagesCount);
                newImage.transform.SetSiblingIndex(newImageIndex);

                objectManager.RecivedRobotsList();

                Objects obj = newImage.GetComponent<Objects>();

                obj.robotUp = 3;
                newImage.name = "Robot " + ObjectManager.totalId;

                GameManager.Instance.State = GameState.Playing;

            }
        }

    }
}
