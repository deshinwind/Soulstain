using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetBola : MonoBehaviour
{
    public GameObject bola;
    public GameObject limiteDelantero;

    public ControladorBolos controladorBolos;
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.GetComponent<BoxCollider>().isTrigger)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                limiteDelantero.SetActive(true);
            }
        }
        else if (collision.gameObject.CompareTag("Bola"))
        {
            Destroy(collision.gameObject);

            controladorBolos.bola = Instantiate(controladorBolos.prefabBola[1]);
            controladorBolos.bola.transform.position = controladorBolos.prefabBola[1].transform.position;
            controladorBolos.bola.transform.rotation = controladorBolos.prefabBola[1].transform.rotation;
            controladorBolos.bola.transform.localScale = controladorBolos.prefabBola[1].transform.localScale;
            controladorBolos.bola.GetComponent<XRGrabInteractable>().enabled = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        limiteDelantero.SetActive(false);
    }
}
