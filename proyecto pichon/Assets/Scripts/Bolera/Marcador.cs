using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Marcador : MonoBehaviour
{
    public TMP_Text[] marcador;

    public Controlador controlador;

    public void ActualizarMarcador()
    {
        marcador[10].text = controlador.bolosTotales.ToString();

        marcador[11].text = controlador.bolosMaxPosibles.ToString();

        marcador[controlador.ronda - 2].text = controlador.bolosEnPie.ToString();
    }
}
