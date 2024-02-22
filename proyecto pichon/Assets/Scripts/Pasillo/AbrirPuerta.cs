using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuerta : MonoBehaviour
{
    public ControladorPasillo controladorPasillo;

    private void OnTriggerEnter(Collider other)
    {
        controladorPasillo.AbrirPuerta();
    }
}
