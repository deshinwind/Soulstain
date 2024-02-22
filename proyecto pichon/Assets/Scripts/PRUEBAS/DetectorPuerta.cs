using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorPuerta : MonoBehaviour
{
    public AudioSource audioSource;

    public GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.AbrirPuerta();
            audioSource.Play();
        }
    }
}
