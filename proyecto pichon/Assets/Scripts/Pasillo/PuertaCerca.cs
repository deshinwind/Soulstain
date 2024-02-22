using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaCerca : MonoBehaviour
{
    public ControladorPasillo controladorPasillo;

    private void OnTriggerEnter(Collider other)
    {
        controladorPasillo.cercaDePuerta = true;
    }
}
