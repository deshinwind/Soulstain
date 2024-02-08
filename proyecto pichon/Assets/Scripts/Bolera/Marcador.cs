using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Marcador : MonoBehaviour
{
    public TMP_Text[] marcador;

    public Controlador controlador;

    public string puntosMaximos = "Puntos Maximos: ";
    public string puntosTotales = "Puntos Totales: ";

    public void ActualizarMarcador()
    {
        marcador[10].text = puntosTotales + controlador.bolosTotales.ToString();

        marcador[11].text = puntosMaximos + controlador.bolosMaxPosibles.ToString();

        marcador[controlador.ronda - 2].text = controlador.bolosRonda.ToString();
    }
}
