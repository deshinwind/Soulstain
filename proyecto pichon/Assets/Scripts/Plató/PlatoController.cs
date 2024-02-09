using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PlatoController : MonoBehaviour
{
    public GameObject panelSolido;

    public GameObject player;

    public Vector3 alphaPanel;

    public GameObject dontDestroy;

    public PlatoDialogos platoDialogo;

    public float wait = 2f;

    public List<string> primerDialogo = new List<string>();
    public List<string> segundoDialogo = new List<string>();
    public List<string> tercerDialogo = new List<string>();

    private void Start()
    {
        alphaPanel = new Vector3(1f, 1f, 1f);

        switch (dontDestroy.GetComponent<DontDestroyOnLoad>().controlador)
        {
            case 0:
                Invoke("PrimeraVezEnPlato", 5f);
                break;
            case 1:
                Invoke("SegundaVezEnPlato", 5f);
                break;
            case 2:
                Invoke("TerceraVezEnPlato", 5f);
                break;
        }
    }

    private void LateUpdate()
    {
        alphaPanel = Vector3.Lerp(alphaPanel, new Vector3(0f, 0f, 0f), 0.01f);
        panelSolido.GetComponent<Image>().color = new Color(panelSolido.GetComponent<Image>().color.r, panelSolido.GetComponent<Image>().color.g, panelSolido.GetComponent<Image>().color.b, alphaPanel.x);
    }

    public void PrimeraVezEnPlato()
    {
        platoDialogo.panelDialogo.SetActive(true);
        platoDialogo.StartDialogue(primerDialogo, wait);
        player.GetComponent<ContinuousTurnProviderBase>().enabled = true;
        panelSolido.SetActive(false);
        //SEGUIR EL BLOC DE NOTAS DE SOULSTAIN



        //AÑADIMOS UNO AL CONTROLADOR ANTES DE CAMBIAR DE ESCENA
        dontDestroy.GetComponent<DontDestroyOnLoad>().controlador++;
    }

    public void SegundaVezEnPlato()
    {
        //SI HAS GANADO TERMINA EL FUNDIDO A BLANCO
        //SI HAS PERDIDO TERMINA EL FUNDIDO A NEGRO
        //EL SILLON DE INVITADOS NO ESTÁ
        //LUZ MAS TENUE DEL SALON
        //TE PUEDES MOVER POR EL ESCENARIO (SOLO POR LA PARTE DEL PLATO)
        //CUANDO MIRAS AL PATIO DE BUTACAS SE ACTIVAN VOCES DE FONTO Y SE PROYECTA UNA SOMBRA DEL MIEDO EN UNA BUTACA
        //QUE APAREZCA EL MIEDO SENTADO EN EL SOFA
        //QUE EL MIEDO SIGA CON LA MIRADA AL PERSONAJE
        //CUANDO EL JUGADOR SE ACERCA LO SUFICIENTE AL MIEDO, CAMBIAS DE REPENTE (SIN TRANSICION) A LA ESCENA DE LA BOLERA
        //EL JUGADOR APAREZCA EN LA BOLERA MIRANDO LA PANTALLA EN LA QUE ESTÁ EL MIEDO

        //AÑADIMOS UNO AL CONTROLADOR ANTES DE CAMBIAR DE ESCENA
        dontDestroy.GetComponent<DontDestroyOnLoad>().controlador++;
    }

    public void TerceraVezEnPlato()
    {

    }
}