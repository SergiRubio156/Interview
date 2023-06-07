using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomImageGenerator : MonoBehaviour
{
    public Texture2D[] images; // Array que contiene las im�genes que quieres mostrar
    public RawImage rawImage; // Referencia al componente RawImage para mostrar las im�genes
    public Texture2D[] pinturas; // Array que contiene las pinturas que quieres mostrar
    public RawImage pinturaImage; // Referencia al componente RawImage para mostrar las pinturas
    public Texture2D[] cpu; // Array que contiene las im�genes de CPU que quieres mostrar
    public RawImage cpuImage; // Referencia al componente RawImage para mostrar las im�genes de CPU
    public TextMeshProUGUI countText; // Referencia al componente de TextMeshProUGUI para el contador
    public Button fetButton; // Referencia al bot�n
    private string randomNumTextTag = "text"; // Tag del objeto de texto para el n�mero aleatorio

    private int count;

    private void Start()
    {
        fetButton.onClick.AddListener(OnClickFetButton);
        GenerateRandomImage();
        GenerateRandomPintura();
        GenerateRandomCPU();
        count = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            IncrementCount();
            GenerateRandomImage();
            GenerateRandomPintura();
            GenerateRandomCPU();
            GenerateRandomNumberWithTag();
        }
    }

    private void OnClickFetButton()
    {
        IncrementCount();
        GenerateRandomImage();
        GenerateRandomPintura();
        GenerateRandomCPU();
        GenerateRandomNumberWithTag();
    }

    public void GenerateRandomImage()
    {
        int randomIndex = Random.Range(0, images.Length);
        rawImage.texture = images[randomIndex];
    }

    public void GenerateRandomPintura()
    {
        int randomIndex = Random.Range(0, pinturas.Length);
        pinturaImage.texture = pinturas[randomIndex];
    }

    public void GenerateRandomCPU()
    {
        int randomIndex = Random.Range(0, cpu.Length);
        cpuImage.texture = cpu[randomIndex];
    }

    public void IncrementCount()
    {
        count++;
        countText.text = count.ToString("D2");
    }

    public void GenerateRandomNumberWithTag()
    {
        TextMeshProUGUI[] randomNumTexts = GameObject.FindObjectsOfType<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in randomNumTexts)
        {
            if (text.tag == randomNumTextTag)
            {
                float randomNumber = Random.Range(1f, 5f);
                randomNumber = Mathf.Round(randomNumber * 2f) / 2f; // Redondea al n�mero m�s cercano con incremento de 0.5 o 1
                text.text = randomNumber.ToString("F1");
            }
        }
    }
}
