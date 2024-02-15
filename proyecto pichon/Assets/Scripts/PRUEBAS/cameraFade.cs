using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFade : MonoBehaviour
{
    public float fadeDuration = 2f; // Duración de la animación de desvanecimiento
    private float fadeTimer = 0f;
    private bool isBroken = false;

    public Material camaraMaterial;
   


    // Start is called before the first frame update
    void Start()
    {
        Color colorcamara = camaraMaterial.color;
        colorcamara.a = 1;
        camaraMaterial.color = colorcamara;
        
    }

    // Update is called once per frame
    public void Update()
    {
        Debug.Log("efwefwef");
        if (fadeTimer < fadeDuration && isBroken)
        {
            // Incrementa el temporizador de desvanecimiento
            fadeTimer += Time.deltaTime;
            // Calcula la nueva transparencia basada en el tiempo transcurrido
            float alpha = 1 - (fadeTimer / fadeDuration);
            // Establece la transparencia en el material
            Color
            color = camaraMaterial.color;
            color.a = alpha;
            camaraMaterial.color = color;
        }

    }
}
