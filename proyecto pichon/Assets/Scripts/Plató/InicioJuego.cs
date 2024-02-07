using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InicioJuego : MonoBehaviour
{
    public TMP_Text dialogos;

    public GameObject negro;

    public List<string> lineas;

    public int indice;
    public bool aux;
    public bool encenderLuz;

    public float speed;
    public float divisor;

    public Vector3 vector;
    public Vector3 normal;

    private void Start()
    {
        encenderLuz = false;
        vector = Vector3.zero;
        normal = new Vector3(1f, 1f, 1f);
        indice = 0;
        aux = true;
        PantallaEnNegro();
    }

    private void LateUpdate()
    {
        if (aux)
        {
            vector = Vector3.Lerp(vector, new Vector3(1f, 0f, 0f), speed / divisor);
        }
        else
        {
            vector = Vector3.Lerp(vector, new Vector3(0f, 0f, 0f), speed);
        }

        dialogos.alpha = vector.x;

        if (encenderLuz)
        {
            normal = Vector3.Lerp(normal, new Vector3(0f, 0f, 0f), 0.01f);
            negro.GetComponent<Image>().color = new Color(negro.GetComponent<Image>().color.r, negro.GetComponent<Image>().color.g, negro.GetComponent<Image>().color.b, normal.x);
        }
    }

    public void PantallaEnNegro()
    {
        if (indice < lineas.Count)
        {
            dialogos.text = lineas[indice];

            indice++;
            Invoke("Cambio", 2.5f);
            if (indice != lineas.Count)
                Invoke("Cambio", 3.5f);
            Invoke("PantallaEnNegro", 3f);
        }

        if (dialogos.text.Equals(""))
        {
            encenderLuz = true;
            dialogos.color = Color.black;
        }
    }

    public void Cambio()
    {
        if (aux)
            aux = false;
        else
            aux = true;
    }
}
