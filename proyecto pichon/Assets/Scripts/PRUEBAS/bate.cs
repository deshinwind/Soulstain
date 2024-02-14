using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bate : MonoBehaviour
{
    public Material mesatext;
    public bool isgrab = false;
    public GameObject mesarota;
    public float fadeDuration = 2f; // Duración de la animación de desvanecimiento
    private float fadeTimer = 0f;
    private bool isBroken = false;

    private void Start()
    {
        Color color = mesatext.color;
        color.a = 1;
        mesatext.color = color;
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
            Color color = mesatext.color;
            color.a = alpha;
            mesatext.color = color;
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
    }

}
