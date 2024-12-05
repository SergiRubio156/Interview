using UnityEngine;

public class ImageInstantiator : MonoBehaviour
{
    public GameObject imagePrefab; // Prefab de la imagen a instanciar
    public GameObject parentObject; // GameObject padre que contiene el componente Horizontal Layout Group

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject newImage = Instantiate(imagePrefab, parentObject.transform);

            // Configurar el índice de orden de la nueva imagen para que aparezca a la derecha de la segunda imagen
            int existingImagesCount = parentObject.transform.childCount - 1; // Restar uno para excluir el propio objeto padre
            int newImageIndex = Mathf.Clamp(existingImagesCount + 1, 0, existingImagesCount);
            newImage.transform.SetSiblingIndex(newImageIndex);

            newImage.name = "comanda " + EvaluateManager.totalId;
        }
    }
}