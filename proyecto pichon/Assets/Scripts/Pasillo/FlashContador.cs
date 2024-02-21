using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashContador : MonoBehaviour
{
    private float puntos;

    private void Start()
    {
        puntos = 0;
    }
    public void sumarpuntos()
    {
        puntos += 1;
        
    }
    private void Update()
    {
        if (puntos > 3)
        {
            Debug.Log("has perdido");
        }
    }
}
