using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

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
    public GameObject[] panicoEscenico;

    public GameObject player;

    public bool letrasBolera = true;

    public bool dialogoFinalMostrado = false;

    public bool dialogoInicalMostrado = false;
    public bool dialogoRonda2Mostrado = false;
    public bool dialogoRonda5Mostrado = false;
    public bool dialogoRonda8Mostrado = false;
    

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
        player.transform.position = new Vector3(-1.290162f, 1.550513f, 2.52f);

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

        if (bolosMaxPosibles < 75)
        {
            //FIN DE LA PARTIDA
            DialogoDerrota();
        }
        else if (ronda > 10)
        {
            //FIN DE PARTIDA
            DialogoVictoria();
        }

        switch (ronda)
        {
            case 1:
                DialogoInicial();
                break;
            case 2:
                DialogoRonda2();
                break;
            case 5:
                DialogoRonda5();
                break;
            case 8:
                DialogoRonda8();
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
        if (!panelLetras.activeSelf && !letrasBolera)
        {
            if (!dialogoInicalMostrado)
            {
                if (dialogueController.pause)
                {
                    int n = Random.Range(0, 1);

                    controladorBolos.bola = Instantiate(controladorBolos.prefabBola[n]);
                    controladorBolos.bola.transform.position = controladorBolos.prefabBola[n].transform.position;
                    controladorBolos.bola.transform.rotation = controladorBolos.prefabBola[n].transform.rotation;
                    controladorBolos.bola.transform.localScale = controladorBolos.prefabBola[n].transform.localScale;
                    controladorBolos.bola.GetComponent<XRGrabInteractable>().enabled = true;

                    dialogueController.pause = false;
                    dialogoInicalMostrado = true;
                }
            }
        }
    }

    public void DialogoRonda2()
    {
        if (!dialogoRonda2Mostrado)
        {
            if (ronda == 2)
            {
                dialogueController.StartDialogue(dialogoRonda2, wait);
                dialogoRonda2Mostrado = true;
            }
        }
        
    }

    public void DialogoRonda5()
    {
        if (!dialogoRonda5Mostrado)
        {
            if (ronda == 5)
            {
                dialogueController.StartDialogue(dialogoRonda5, wait);
                dialogoRonda5Mostrado = true;
            }
        }
    }

    public void DialogoRonda8()
    {
        if (!dialogoRonda8Mostrado)
        {
            if (ronda == 8)
            {
                dialogueController.StartDialogue(dialogoRonda8, wait);
                dialogoRonda8Mostrado = true;
            }
        }
    }

    public void DialogoVictoria()
    {
        //HACER LAS COMPROBACIONES DE SI ESTA MOSTRANDO TEXTO MIRANDO SI ESTA ACTIVO EL PANEL DE DIALOGOS
        if (!dialogoFinalMostrado)
        {
            dialogueController.StartDialogue(dialogoVictoria, wait);
            dialogoFinalMostrado = true;
        }
        else if (dialogueController.pause)
        {
            //TRANSICION AL PLATO
            SceneManager.LoadScene("Plato");
        }
    }

    public void DialogoDerrota()
    {
        //SE APAGA LA LUZ QUE ILUMINA LA CALLE DE LOS BOLOS
        if (!dialogoFinalMostrado)
        {
            dialogoFinalMostrado = true;
            dialogueController.StartDialogue(dialogoDerrota, wait);

            foreach (GameObject miedo in panicoEscenico)
            {
                miedo.SetActive(true);
            }
        }
        else if (panicoEscenico[1].activeSelf)
        {
            if (dialogueController.pause)
            {
                for (int i = 1; i < panicoEscenico.Length; i++)
                {
                    panicoEscenico[i].SetActive(false);
                }
                dialogueController.pause = false;
            }
        }
        else if (dialogueController.pause)
        {
            //SE QUEDA TODO A OSCURAS
            //TRANSICION A EL PLATO
            SceneManager.LoadScene("Plato");
        }
    }
}
