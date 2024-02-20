using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class PlatoController : MonoBehaviour
{
    public GameObject panelSolido;
    public DialogueController platoDialogo;
    public GameObject panelLetras;
    public GameObject player;
    public GameObject claustrofobia;
    public GameObject techo;
    public bool letras = true;
    public bool latiendo;
    public AudioClip latido;

    public GameObject sillon;
    public GameObject miedo2;
    public GameObject sombra;

    public GameObject[] camara;

    public float speed;

    public bool salaPequeña = false;

    public Vector3 alphaPanelWhite = new Vector3(1f, 1f, 1f);
    public Vector3 alphaPanelBlack = new Vector3(0f, 0f, 0f);

    public GameObject dontDestroy;

    public PlatoRayCast rayCast;

    public float wait = 2f;

    public List<string> primerDialogo = new List<string>();
    public List<string> segundoDialogo = new List<string>();
    public List<string> tercerDialogo = new List<string>();

    public float timer = 0f;

    private void Start()
    {

        switch (dontDestroy.GetComponent<DontDestroyOnLoad>().controlador)
        {
            case 0:
                Invoke("PrimeraVezEnPlato", 5f);
                break;
            case 1:
                //BAJAR LA LUZ EN ESTA ESCENA
                Invoke("SegundaVezEnPlato", 5f);
                break;
            case 2:
                Invoke("TerceraVezEnPlato", 5f);
                break;
        }
    }

    private void Update()
    {
        switch (dontDestroy.GetComponent<DontDestroyOnLoad>().controlador)
        {
            case 0:
                PrimeraComprobacion();
                break;
            case 1:
                SegundaComprobacion();
                break;
            case 2:
                TerceraComprobacion();
                break;
        }
    }

    private void LateUpdate()
    {
        if (!claustrofobia.activeSelf)
        {
            alphaPanelWhite = Vector3.Lerp(alphaPanelWhite, new Vector3(0f, 0f, 0f), 0.05f);
            panelSolido.GetComponent<Image>().color = new Color(panelSolido.GetComponent<Image>().color.r, panelSolido.GetComponent<Image>().color.g, panelSolido.GetComponent<Image>().color.b, alphaPanelWhite.x);
        }
    }

    public void PrimeraVezEnPlato()
    {
        platoDialogo.panelDialogo.SetActive(true);
        platoDialogo.StartDialogue(primerDialogo, wait);
        player.GetComponent<ContinuousTurnProviderBase>().enabled = true;
        panelSolido.SetActive(false);
        //SEGUIR EL BLOC DE NOTAS DE SOULSTAIN
    }

    public void PrimeraComprobacion()
    {
        rayCast.CastRay();
        //QUITAR COMENTARIO CUANDO PONGA AUDIO
        //if (latiendo)
        //{
        //SUBIR EL VOLUMEN DEL AUDIOSOURCE
        //latido.volume += 0.1f;
        //}

        if (Physics.Raycast(rayCast.ray, out rayCast.hit))
        {
            Debug.Log(rayCast.hit.transform.gameObject.name.Equals("Claustrofobia"));
            Debug.Log(letras);
            if (rayCast.hit.transform.gameObject.name.Equals("Techo"))
            {
                if (platoDialogo.pause && !claustrofobia.activeSelf)
                {
                    rayCast.hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    platoDialogo.pause = false;
                    claustrofobia.SetActive(true);

                    //EMPIEZA A HACERSE LA SALA PEQUEÑA POCO A POCO
                    salaPequeña = true;
                }
                //Debug.Log(rayCast.hit.transform.gameObject.name);
            }
            else if (rayCast.hit.transform.gameObject.name.Equals("Claustrofobia") && letras)
            {
                panelLetras.SetActive(true);
                Invoke("DesactivarLetras", 2f);

                //QUITAR COMENTARIO CUANDO PONGA AUDIO
                //A PARTIR DE AQUI TIENE QUE EMPEZAR A SONAR EL LATIDO DEL CORAZON CADA VEZ MAS FUERTE
                //latiendo = true;
                //latido.Play();
            }
        }

        if (salaPequeña && claustrofobia.activeSelf)
        {
            techo.transform.position = Vector3.Lerp(techo.transform.position, new Vector3(techo.transform.position.x, 3f, techo.transform.position.z), speed);
        }

        if (platoDialogo.pause && claustrofobia.activeSelf)
        {
            if (!panelSolido.activeSelf)
            {
                panelSolido.GetComponent<Image>().color = Color.black;
                panelSolido.gameObject.SetActive(true);
                Invoke("PrimeraTransicion", 3f);
            }
            //PONER EL EFECTO DE POSPROCESADO PARA QUE PAREZCA QUE ESTÁ CERRANDO LOS OJOS EL JUGADOR
            alphaPanelBlack = Vector3.Lerp(alphaPanelBlack, new Vector3(1f, 0f, 0f), 0.01f);
            panelSolido.GetComponent<Image>().color = new Color(panelSolido.GetComponent<Image>().color.r, panelSolido.GetComponent<Image>().color.g, panelSolido.GetComponent<Image>().color.b, alphaPanelBlack.x);
        }
    }

    public void DesactivarLetras()
    {
        panelLetras.SetActive(false);
        letras = false;
    }

    public void PrimeraTransicion()
    {
        dontDestroy.GetComponent<DontDestroyOnLoad>().controlador++;
        SceneManager.LoadScene("BlackJack");
    }

    public void SegundaVezEnPlato()
    {
        //SI HAS GANADO TERMINA EL FUNDIDO A BLANCO
        //SI HAS PERDIDO TERMINA EL FUNDIDO A NEGRO
        panelSolido.SetActive(false);
        //EL SILLON DE INVITADOS NO ESTA
        sillon.SetActive(false);

        //LUZ MAS TENUE DEL SALON
        //TE PUEDES MOVER POR EL ESCENARIO (SOLO POR LA PARTE DEL PLATO)
        player.GetComponent<ContinuousMoveProviderBase>().enabled = true;
        player.GetComponent<ContinuousTurnProviderBase>().enabled = true;
        player.transform.rotation = Quaternion.Euler(0f, 285.432f, 0f);
        //CUANDO MIRAS AL PATIO DE BUTACAS SE ACTIVAN VOCES DE FONTO Y SE PROYECTA UNA SOMBRA DEL MIEDO EN UNA BUTACA
        //QUE APAREZCA EL MIEDO SENTADO EN EL SOFA
        //QUE EL MIEDO SIGA CON LA MIRADA AL PERSONAJE
        //CUANDO EL JUGADOR SE ACERCA LO SUFICIENTE AL MIEDO, CAMBIAS DE REPENTE (SIN TRANSICION) A LA ESCENA DE LA BOLERA
        //EL JUGADOR APAREZCA EN LA BOLERA MIRANDO LA PANTALLA EN LA QUE ESTÁ EL MIEDO
    }

    public void SegundaComprobacion()
    {
        rayCast.CastRay();

        Debug.Log(Physics.Raycast(rayCast.ray, out rayCast.hit));

        if (Physics.Raycast(rayCast.ray, out rayCast.hit))
        {
            Debug.Log(rayCast.hit.transform.gameObject.name);
            if (rayCast.hit.transform.gameObject.name.Equals("Butacas"))
            {
                sombra.SetActive(true);
                //ENCENDER EL FOCO DE LA SOMBRA
                miedo2.SetActive(true);
            }
        }

        if (miedo2.activeSelf)
        {
            miedo2.transform.LookAt(player.transform);

            //AUMENTAR LA DISTANCIA

            if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(miedo2.transform.position.x, miedo2.transform.position.y)) <= 1f)
            {
                SegundaTransicion();
            }
        }
    }

    public void SegundaTransicion()
    {
        //AÑADIMOS UNO AL CONTROLADOR ANTES DE CAMBIAR DE ESCENA
        dontDestroy.GetComponent<DontDestroyOnLoad>().controlador++;
        SceneManager.LoadScene("Bolera");
    }

    public void TerceraVezEnPlato()
    {
        player.GetComponent<ActionBasedContinuousTurnProvider>().enabled = true;    //PREGUNTARLE A JUANAN SI ES ESTE O EL CONINUOUSTURNPROVIDER
        player.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;    //PREGUNTARLE A JAVI SI PUEDE MOVERSE EN ESTA PARTE DEL JUEGO
    }

    public void TerceraComprobacion()
    {
        foreach (GameObject item in camara)
        {
            item.transform.LookAt(player.transform, Vector3.left);
        }

        rayCast.CastRay();

        if (player.GetComponent<ActionBasedContinuousTurnProvider>().enabled)
        {
            if (Physics.Raycast(rayCast.ray, out rayCast.hit))
            {
                Debug.Log(rayCast.hit.transform.gameObject.name);
                if (rayCast.hit.transform.gameObject.CompareTag("Camara"))
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    timer = 0f;
                }
            }
            else
            {
                timer = 0f;
            }
        }

        if (timer >= 1f)
        {
            player.GetComponent<ActionBasedContinuousTurnProvider>().enabled = false;
            player.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
            //HACER EL EFECTO DE LA CAMARA Y LA TRANSICION
            SceneManager.LoadScene("PASILLO");
        }
    }
}