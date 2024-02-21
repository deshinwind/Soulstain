using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaPasillo : MonoBehaviour
{
    private Bate bate;

    [Header("Mesa")]
    public Material mesaMaterial;
    public GameObject mesarota;

    [Header("Fade")]
    public float fadeDuration = 5f; // Duración de la animación de desvanecimiento
    private float fadeTimer = 0f;
    private bool isBroken = false;


    // Start is called before the first frame update
    void Start()
    {
        bate = FindAnyObjectByType<Bate>();
        Color colormesa = mesaMaterial.color;
        colormesa.a = 1;
        mesaMaterial.color = colormesa;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeTimer < fadeDuration && isBroken)
        {
            // Incrementa el temporizador de desvanecimiento
            fadeTimer += Time.deltaTime;
            // Calcula la nueva transparencia basada en el tiempo transcurrido
            float alpha = 1 - (fadeTimer / fadeDuration);
            // Establece la transparencia en el material
            Color color = mesaMaterial.color;
            color.a = alpha;
            mesaMaterial.color = color;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bate") && bate.isgrab)
        {
            isBroken = true;
            mesarota = Instantiate(mesarota, collision.transform.position, mesarota.transform.rotation);
            Destroy(mesarota, fadeDuration - 0.5f);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(this.gameObject, fadeDuration - 0.5f);
        }

    }
}

