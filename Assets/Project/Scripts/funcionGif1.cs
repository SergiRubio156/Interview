using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class funcionGif1 : MonoBehaviour
{
    public float duration;
    public Sprite[] sprites;

    private Image image;
    private int index = 0;
    private float timer = 0;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = sprites[index];
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float frameDuration = duration / sprites.Length;

        if (timer >= frameDuration)
        {
            timer -= frameDuration;
            index = (index + 1) % sprites.Length;
            image.sprite = sprites[index];
        }
    }
}
