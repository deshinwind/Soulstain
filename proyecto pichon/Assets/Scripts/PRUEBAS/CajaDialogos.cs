using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CajaDialogos : MonoBehaviour
{
    public TMP_Text texto;

    public List<string> lineas;

    public Vector3 vector;

    public float speed;

    public char letrica;

    private void Start()
    {
        vector = new Vector3(17f, 0f, 0f);
        GuluGulu();
    }

    private void Update()
    {
        vector = Vector3.Lerp(vector, new Vector3(745f, 0f, 0f), speed);
    }

    public void GuluGulu()
    {
        foreach (var letra in lineas[0])
        {
            Debug.Log(letra); 
            letrica = letra;
            Invoke("AñadirLetra", 0.5f);
        }
    }

    public void AñadirLetra()
    {
        texto.text += letrica;
    }
}
