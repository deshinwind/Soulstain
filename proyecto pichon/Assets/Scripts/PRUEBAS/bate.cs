using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bate : MonoBehaviour
{    
    
    public bool isgrab = false;

    [Header("Mesa")]
    public Material mesaMaterial;
    public GameObject mesarota;

    [Header("Camara")]
    public Material camaraMaterial;
    public GameObject camaraRota;
    public cameraFade camefade;

    [Header("Fade")]
    public float fadeDuration = 2f; // Duración de la animación de desvanecimiento
    private float fadeTimer = 0f;
    private bool isBroken = false;

    private void Start()
    {
        Color colormesa = mesaMaterial.color;
        colormesa.a = 1;
        mesaMaterial.color = colormesa;

        Color colorcamara = mesaMaterial.color;
        colorcamara.a = 1;
        mesaMaterial.color = colorcamara;
    }
    public void agarrao()
    {
        isgrab = true;
    }

    public void noagarrao()
    {
        isgrab=false;
    }

    private void Update()
    {
        if (fadeTimer < fadeDuration && isBroken)
        {
            // Incrementa el temporizador de desvanecimiento
            fadeTimer += Time.deltaTime;
            // Calcula la nueva transparencia basada en el tiempo transcurrido
            float alpha = 1 - (fadeTimer / fadeDuration);
            // Establece la transparencia en el material
            Color 
            color = mesaMaterial.color;
            color = camaraMaterial.color;
            color.a = alpha;
            mesaMaterial.color = color;
            camaraMaterial.color = color;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "mesa" && isgrab)
        {
            isBroken = true;
            mesarota = Instantiate(mesarota, collision.transform.position, mesarota.transform.rotation);
            Destroy(mesarota, fadeDuration-1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("camara") && isgrab)
        {
            isBroken = true;
            camaraRota = Instantiate(camaraRota, collision.transform.position, camaraRota.transform.rotation);
            Destroy(camaraRota, 5f);
            Destroy(collision.gameObject);
            camefade.Update();
        }
    }

}
