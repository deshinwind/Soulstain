using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ControladorBJ : MonoBehaviour
{
    public GameObject player;
    public GameObject panelSolido;

    public Partida partida;

    public DialogueController dialogueController;

    public int ronda = 0;

    public Vector3 alphaPanelBlack = new Vector3(1f, 1f, 1f);

    public bool fundidoNegro = true;
    public bool tutorial = false;
    public bool finDeTutorial = false;

    public float wait = 2f;

    public List<string> dialogoTutorial = new List<string>();
    public List<string> dialogoPrimeraRonda = new List<string>();


    //EL JUGADOR NO PUEDE MOVERSE DEL SITIO


    private void Start()
    {
        partida.puntosJugador = 0;
        Invoke("ActivarRotacionPersonaje", 2f);
        Invoke("IniciarDialogos", 7f);
    }

    private void Update()
    {
        //switch para rondas
        if (!finDeTutorial)
        {
            if (dialogueController.pause && partida.manoJugador.Count == 0)
            {
                partida.rondaEnJuego = true;
                dialogueController.pause = false;
                NuevaCartaJugador();
                Invoke("NuevaCartaJugador", 3f);
            }
            if (dialogueController.pause && partida.manoJugador.Count == 2 && partida.manoDealer.Count == 0)
            {
                dialogueController.pause = false;
                NuevaCartaDealer();
                Invoke("NuevaCartaDealer", 3f);
            }
            if (dialogueController.pause && partida.manoDealer.Count == 2)
            {
                dialogueController.pause = false;
                partida.ComprobarPuntosJugador();

                //*********************************************//
                //ESTO A LO MEJOR SE PUEDE QUITAR MAS A DELANTE//
                //*********************************************//
                partida.ComprobarPuntosDealer();
                finDeTutorial = true;
                //ronda++;
            }
        }
    }

    private void LateUpdate()
    {
        if (fundidoNegro)
        {
            //EMPIEZA CON EL FUNDIDO A NEGRO Y EL EFECTO DE ABRIR LOS OJOS
            //METER EL EFECTO DE LOS OJOS ABRIENDO
            alphaPanelBlack = Vector3.Lerp(alphaPanelBlack, new Vector3(0f, 0f, 0f), 0.05f);
            panelSolido.GetComponent<Image>().color = new Color(panelSolido.GetComponent<Image>().color.r, panelSolido.GetComponent<Image>().color.g, panelSolido.GetComponent<Image>().color.b, alphaPanelBlack.x);
        }
        else if (panelSolido.activeSelf)
        {
            panelSolido.gameObject.SetActive(false);
        }
    }

    public void ActivarRotacionPersonaje()
    {
        player.GetComponent<ContinuousTurnProviderBase>().enabled = true;
        fundidoNegro = false;
    }

    public void IniciarDialogos()
    {
        //LINEA DE LA CLAUSTROFOBIA
        dialogueController.panelDialogo.SetActive(true);
        dialogueController.StartDialogue(dialogoTutorial, wait);

        //TERMINA LA LINEA DE LA CLAUSTROFOBIA Y SE EMPIEZAN A REPARTIR LAS CARTAS
    }

    public void InicioTutorial()
    {
        tutorial = true;
        NuevaCartaJugador();
        Invoke("NuevaCartaJugador", 1.5f);
    }

    public void NuevaCartaJugador()
    {
        partida.AñadirCarta(partida.manoJugador);
    }

    public void NuevaCartaDealer()
    {
        partida.AñadirCarta(partida.manoDealer);
    }

    //TERMINA LA LINEA DE LA CLAUSTROFOBIA Y SE EMPIEZAN A REPARTIR LAS CARTAS
    //DOS LINEAS DE LA CLAUSTROFOBIA MIENTRAS SE REPARTEN LAS CARTAS
    //CUANDO VA A DECIR LA TERCERA LINEA, LAS CARTAS YA SE HAN REPARTIDO (EN ESTA LINEA SE EXPLICA EL FUNCIONAMINETO DEL MINIJUEGO)
    //OTRAS DOS LINEAS DE LA CLAUSTROFOBIA
    //SE JUEGA LA PARTIDA DE PRUEBA HASTA QUE TERMINE
    //UNA VEZ TERMINADA LA PARTIDA DE PRUEBA, EMPIEZA LA PRIMERA RONDA (AUN SIN REPARTIR LAS CARTAS)
    //LINEA DE CLAUSTROFOBIA
    //SE REPARTEN LAS CARTAS AL TERMINAR LA LINEA
    //TRES LINEAS MIENTRAS SE JUEGA LA RONDA

    //SI GANA QUE SE MUESTREN UNOS DIALOGOS. SI PIERDE OTROS

    //LA PARTIDA ES AL MEJOR DE 5 RONDAS
    //PARA CADA VICTORIA / DERROTA LAS PAREDES SE VAN A IR A UNA POSICION CONCRETA, DE MANERA QUE QUEDE MAS PATENTE QUE A CADA RONDA SE ESTÁ LLEGANDO AL FINAL
    //V1 -> 1.1 // V2 -> 0.6 // V3 -> 0.0   ////    D1 -> 2.0 // D2 -> 2.4 // D3 -> 2.8

    //SI GANAS / PIERDES MOSTRAR DIALOGO DE GANAR / PERDER HACIENDO LA TRANSICION CORRESPONDIENTE AL PLATO
}
