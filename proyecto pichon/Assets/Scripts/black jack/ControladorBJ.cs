using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ControladorBJ : MonoBehaviour
{
    public GameObject player;
    public GameObject panelSolido;

    public Vector3 alphaPanelBlack = new Vector3(1f, 1f, 1f);

    public bool fundidoNegro = true;

    private void Start()
    {
        Invoke("ActivarRotacionPersonaje", 2f);
    }

    private void LateUpdate()
    {
        if (fundidoNegro)
        {
            alphaPanelBlack = Vector3.Lerp(alphaPanelBlack, new Vector3(0f, 0f, 0f), 0.05f);
            panelSolido.GetComponent<Image>().color = new Color(panelSolido.GetComponent<Image>().color.r, panelSolido.GetComponent<Image>().color.g, panelSolido.GetComponent<Image>().color.b, alphaPanelBlack.x);
        }
    }

    public void ActivarRotacionPersonaje()
    {
        player.GetComponent<ContinuousTurnProviderBase>().enabled = true;
        fundidoNegro = false;
    }

    //EL JUGADOR NO PUEDE MOVERSE DEL SITIO
    //EMPIEZA CON EL FUNDIDO A NEGRO Y EL EFECTO DE ABRIR LOS OJOS
    //LINEA DE LA CLAUSTROFOBIA
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

    //
}
