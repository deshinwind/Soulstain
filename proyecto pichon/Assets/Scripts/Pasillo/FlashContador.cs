using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashContador : MonoBehaviour
{
    public int puntos;
    public GameObject ciegoUI;
    private void Start()
    {
        puntos = 0;
        
    }
    public void sumarpuntos()
    {
        puntos += 1;
        ciegoUI.SetActive(true);
    }
}
