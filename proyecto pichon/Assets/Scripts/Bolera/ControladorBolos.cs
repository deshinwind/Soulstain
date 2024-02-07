using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class ControladorBolos : MonoBehaviour
{
    public GameObject brazo;
    public GameObject bola;
    public GameObject[] prefabBola;
    public GameObject[] bolo;
    public GameObject[] prefavBolos;
    public GameObject cielo;

    //public AudioClip voces;
    //public List<AudioClip> grupoVoces;

    public GameObject voces;

    public List<GameObject> grupoVoces;

    public Controlador controlador;

    public Marcador marcador;

    public bool brazoBajado;
    public bool recogerBolos;
    public bool colocandoBolos;
    public bool bolosCreados;

    public float posicionEnXVUELTA = 18f;
    public float posicionEnXIDA = 19.25f;
    public float speed;
    public float subeBrazo = 0.518f;
    public float bajaBrazo = 1.21f;

    public bool bolaLanzada;

    private void Start()
    {
        brazoBajado = false;
        recogerBolos = false;
        colocandoBolos = false;
        bolosCreados = false;
        bolaLanzada = false;

        grupoVoces = new List<GameObject>();
    }

    private void LateUpdate()
    {
        if (brazoBajado)
        {
            //BAJA EL BRAZO
            brazo.transform.position = Vector3.Lerp(brazo.transform.position, new Vector3(brazo.transform.position.x, subeBrazo, brazo.transform.position.z), speed);
        }
        else
        {
            //SUBE EL BRAZO
            brazo.transform.position = Vector3.Lerp(brazo.transform.position, new Vector3(brazo.transform.position.x, bajaBrazo, brazo.transform.position.z), speed);
        }

        if (recogerBolos)
        {
            //RECOGE BOLOS
            brazo.transform.position = Vector3.Lerp(brazo.transform.position, new Vector3(posicionEnXIDA, brazo.transform.position.y, brazo.transform.position.z), speed);
        }
        else
        {
            //VUELVE EL BRAZO
            brazo.transform.position = Vector3.Lerp(brazo.transform.position, new Vector3(posicionEnXVUELTA, brazo.transform.position.y, brazo.transform.position.z), speed);
        }
        if (colocandoBolos)
        {
            if (bolosCreados)
            {
                for(int i = 0; i < bolo.Length; i++)
                {
                    bolo[i].transform.position = Vector3.Lerp(bolo[i].transform.position, new Vector3(bolo[i].transform.position.x, 0.76f, bolo[i].transform.position.z), 0.01f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bola"))
        {
            bolaLanzada = true;
            //ESPERAR UNOS SEGUNDOS PARA QUE SE CAIGAN LOS BOLOS
            Invoke("BajarBrazo", 1f);
            Invoke("ContarBolos", 5f);
            Invoke("RecogerBolos", 6f);
            Invoke("AñadirVoces", 6f);
            Invoke("DevolverBola", 7f);
            Invoke("ColocarBolos", 11f);
            Invoke("SubirBrazo", 12f);
        }
    }

    private void ContarBolos()
    {
        controlador.bolosEnPie = 0;
        controlador.bolosRonda = 0;

        for(int i = 0; i < bolo.Length; i++)
        {
            Debug.Log(bolo[i] == null);
            if (bolo[i] != null)
            {
                if (bolo[i].GetComponent<RayCastBolos>().ComprobarBolo()) //Comprueba si el bolo está mirando al cielo
                {
                    controlador.bolosEnPie++;
                    //Debug.Log("BE:" + controlador.bolosEnPie);
                }
                else
                {
                    controlador.bolosRonda++;
                    //Debug.Log("BT:" + controlador.bolosTotales);
                }
            }
            else
            {
                controlador.bolosRonda++;
                //Debug.Log("BT:" + controlador.bolosTotales);
            }
        }
        controlador.bolosTotales += controlador.bolosRonda;
        controlador.bolosMaxPosibles -= controlador.bolosEnPie;
        controlador.ronda++;
        marcador.ActualizarMarcador();
    }

    private void BajarBrazo()
    {
        brazoBajado = true;
        //ACTIVAR LA ANIMACION DE BAJAR EL BRAZO
    }

    private void RecogerBolos()
    {
        //ACTIVAR LA ANIMACION PARA QUE EL BRAZO TIRE LOS BOLOS AL DESTRUCTOR DE MUNDOS
        //ACTIVAR LA ANIMACION PARA QUE EL BRAZO VUELA A LA POSICION DE BRAZO BAJADO
        if (recogerBolos)
        {
            recogerBolos = false;
        }
        else
        {
            recogerBolos = true;
            Invoke("RecogerBolos", 3f);
        }
    }

    private void AñadirVoces()
    {
        //AÑADIR UNA VOZ ALEATORIA POR CADA BOLO QUE HAYA QUEDADO EN PIE
        for (int i = 0; i < controlador.bolosEnPie; i++)
        {
            grupoVoces.Add(Instantiate(voces));
            grupoVoces[grupoVoces.Count - 1].transform.position = new Vector3(Camera.main.transform.position.x * Random.Range(-2f, 2f), Camera.main.transform.position.y * Random.Range(0f, 2f), Camera.main.transform.position.z * Random.Range(-2f, 2f));
            grupoVoces[grupoVoces.Count - 1].GetComponent<AudioSource>().volume = Random.Range(0.5f, 1f);
            grupoVoces[grupoVoces.Count - 1].GetComponent<AudioSource>().pitch = Random.Range(0.1f, 1.5f);
        }
    }

    private void DevolverBola()
    {
        if (bola != null)
            Destroy(bola);

        //INSTANCIAR EL PREFAV DE LA BOLA
        int n = Random.Range(0, 1);

        bola = Instantiate(prefabBola[n]);
        bola.transform.position = prefabBola[n].transform.position;
        bola.transform.rotation = prefabBola[n].transform.rotation;
        bola.transform.localScale = prefabBola[n].transform.localScale;
    }

    private void ColocarBolos()
    {
        DestruirBolosRestantes();

        colocandoBolos = true;
        bolosCreados = false;
        cielo.GetComponent<BoxCollider>().enabled = false;
        //INSTANCIAR EL PREFAV DE LOS BOLOS DESACTIVANDO EL RIGIDBODY PARA QUE NO CAIGAN POR LA GRAVEDAD Y HACER QUE BAJEN POCO A POCO HASTA LA PISTA
        //FINALMENTE ACTIVAR EL RIGIDBODY PARA QUE VUELVA A AFECTARLES LA GRAVEDAD
        for(int i = 0; i < prefavBolos.Length;i++)
        {
            bolo[i] = Instantiate(prefavBolos[i]);
            bolo[i].transform.position = prefavBolos[i].transform.position;
            bolo[i].transform.rotation = prefavBolos[i].transform.rotation;
            bolo[i].transform.localScale = prefavBolos[i].transform.localScale;
        }
        bolosCreados = true;
        Invoke("ActivarCielo", 3f);
    }

    public void DestruirBolosRestantes()
    {
        foreach (var bolo in bolo)
        {
            if (bolo != null)
            {
                Destroy(bolo);
            }
        }
    }

    private void ActivarCielo()
    {
        cielo.GetComponent<BoxCollider>().enabled = true;
        colocandoBolos = false;

        for(int i = 0; i < bolo.Length;i++)
        {
            bolo[i].GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void SubirBrazo()
    {
        brazoBajado = false;
        Debug.Log("Puntuacion: " + controlador.bolosTotales);
        Debug.Log("Bolos en pie: " + controlador.bolosEnPie);
        Debug.Log("Puntuacion Maxima: " + controlador.bolosMaxPosibles);
        Debug.Log("Ronda: " + controlador.ronda);

        bolaLanzada = false;

        bola.GetComponent<XRGrabInteractable>().enabled = true;
        //ACTIVAR LA ANIMACOIN DE SUBIR EL BRAZO PARA QUE SE PUEDAN COLOCAR LOS BOLOS Y VOLVER A JUGAR
    }
}
