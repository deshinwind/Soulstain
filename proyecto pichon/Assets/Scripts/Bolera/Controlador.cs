using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlador : MonoBehaviour
{
    public int ronda;
    public int bolosTotales;
    public int bolosMaxPosibles;
    public int bolosEnPie;
    public int bolosRonda;

    public DialogueController dialogueController;

    public ControladorBolos controladorBolos;

    public RayCastPlayer playerRayCast;

    public GameObject panelLetras;
    public GameObject panicoEscenico;

    public bool letrasBolera = true;

    public bool derrotaMostrada = false;

    public float timer;

    public float wait = 2f;

    public int numeroDialogo;

    public List<string> dialogoInicial = new List<string>();
    public List<string> dialogoRonda2 = new List<string>();
    public List<string> dialogoRonda5 = new List<string>();
    public List<string> dialogoRonda8 = new List<string>();
    public List<string> dialogoVictoria = new List<string>();
    public List<string> dialogoDerrota = new List<string>();

    void Start()
    {
        numeroDialogo = 0;
        timer = 0f;
        ronda = 1;
        bolosTotales = 0;
        bolosMaxPosibles = 100;
    }

    void Update()
    {
        if (letrasBolera)
        {
            CastRay();
        }

        if (ronda > 10 || bolosMaxPosibles < 75)
        {
            //FIN DE LA PARTIDA
            DialogoDerrota();
        }

        switch (numeroDialogo)
        {
            case 0:
                DialogoInicial();
                break;
            case 1:
                DialogoRonda2();
                break;
            case 2:
                DialogoRonda5();
                break;
            case 3:
                DialogoRonda8();
                break;
            case 4:
                DialogoVictoria();
                break;
            default:
                break;
        }
    }

    public void CastRay()
    {
        playerRayCast.CastRay();

        if (Physics.Raycast(playerRayCast.ray, out playerRayCast.hit))
        {
            if (playerRayCast.hit.transform.gameObject.name.Equals("Bolera"))
            {
                panelLetras.SetActive(true);
                Invoke("DesactivarLetras", 2f);
                letrasBolera = false;
            }
        }
    }

    public void DesactivarLetras()
    {
        dialogueController.panelDialogo.SetActive(true);
        dialogueController.StartDialogue(dialogoInicial, wait);
        panelLetras.SetActive(false);
    }

    public void DialogoInicial()
    {
        if (dialogueController.pause)
        {
            int n = Random.Range(0, 1);

            controladorBolos.bola = Instantiate(controladorBolos.prefabBola[n]);
            controladorBolos.bola.transform.position = controladorBolos.prefabBola[n].transform.position;
            controladorBolos.bola.transform.rotation = controladorBolos.prefabBola[n].transform.rotation;
            controladorBolos.bola.transform.localScale = controladorBolos.prefabBola[n].transform.localScale;
            dialogueController.pause = false;
            numeroDialogo++;
        }
    }

    public void DialogoRonda2()
    {
        if (ronda == 2 && !dialogueController.isShowingText)
        {
            dialogueController.StartDialogue(dialogoRonda2, wait);
            numeroDialogo++;
        }
    }

    public void DialogoRonda5()
    {
        if (ronda == 5 && !dialogueController.isShowingText)
        {
            dialogueController.StartDialogue(dialogoRonda5, wait);
            numeroDialogo++;
        }
    }

    public void DialogoRonda8()
    {
        if (ronda == 8 && !dialogueController.isShowingText)
        {
            dialogueController.StartDialogue(dialogoRonda8, wait);
            numeroDialogo++;
        }
    }

    public void DialogoVictoria()
    {
        //SE ENCIENDE EL FOCO DEL FINAL DE LA BOLERA. NO ENTIENDO CUAL ES
        //EL JUGADOR SE VA A ACERCAR?? EN TRINCIPIO VA A HABAR UN COLLIDER PARA QUE NO PUEDA METERSE EN LA PISTA

        //HACER LAS COMPROBACIONES DE SI ESTA MOSTRANDO TEXTO MIRANDO SI ESTA ACTIVO EL PANEL DE DIALOGOS
        if (!dialogueController.isShowingText)
        {
            dialogueController.StartDialogue(dialogoVictoria, wait);
            numeroDialogo++;
        }
    }

    public void DialogoDerrota()
    {
        //SE APAGA LA LUZ QUE ILUMINA LA CALLE DE LOS BOLOS
        if (!derrotaMostrada)
        {
            if (!dialogueController.isShowingText)
            {
                derrotaMostrada = true;
                dialogueController.StartDialogue(dialogoDerrota, wait);
            }
        }
        else if (!panicoEscenico.activeSelf)
        {
            if (dialogueController.pause)
            {
                panicoEscenico.SetActive(true);
            }
        }
        else if (!dialogueController.isShowingText)
        {
            //SE QUEDA TODO A OSCURAS
            //TRANSICION A EL PLATO
            SceneManager.LoadScene("Plato");
        }
    }
}
