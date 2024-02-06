using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barreras : MonoBehaviour
{
    public GameObject barreraIzq;
    public GameObject barreraDer;

    private ControladorBolos controlador;

    public float speed;

    public float subeBarrera;
    public float bajaBarrera;

    public bool botonPulsado;

    private void Start()
    {
        controlador = GameObject.Find("Detector bola").GetComponent<ControladorBolos>();
    }

    private void LateUpdate()
    {
        if (botonPulsado)
        {
            barreraIzq.transform.position = Vector3.Lerp(barreraIzq.transform.position, new Vector3(barreraIzq.transform.position.x, bajaBarrera, barreraIzq.transform.position.z), speed);
            barreraDer.transform.position = Vector3.Lerp(barreraDer.transform.position, new Vector3(barreraIzq.transform.position.x, bajaBarrera, barreraDer.transform.position.z), speed);
        }
        else
        {
            barreraIzq.transform.position = Vector3.Lerp(barreraIzq.transform.position, new Vector3(barreraIzq.transform.position.x, subeBarrera, barreraIzq.transform.position.z), speed);
            barreraDer.transform.position = Vector3.Lerp(barreraDer.transform.position, new Vector3(barreraIzq.transform.position.x, subeBarrera, barreraDer.transform.position.z), speed);
        }
    }

    public void BotonBarreras()
    {
        if (!controlador.colocandoBolos)
        {
            if (!botonPulsado)
                botonPulsado = true;
            else
                botonPulsado = false;
        }
    }
}
