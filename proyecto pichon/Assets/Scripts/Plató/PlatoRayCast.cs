using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatoRayCast : MonoBehaviour
{
    public PlatoDialogos platoDialogos;

    public GameObject panelLetras;
    public GameObject claustrofobia;

    private bool letras = true;
    
    private void Update()
    {
        RaycastHit hite;

        Ray raye = new Ray(transform.position, transform.forward);
        Debug.DrawRay(raye.origin, raye.direction * 30f, Color.green);

        if (Physics.Raycast(raye, out hite))
        {
            if (hite.transform.gameObject.name.Equals("Techo"))
            {
                if (platoDialogos.pause)
                {
                    hite.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    platoDialogos.pause = false;
                    claustrofobia.SetActive(true);

                    //EMPIEZA A HACERSE LA SALA PEQUEÑA POCO A POCO
                }
                //hite.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                //gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                Debug.Log(hite.transform.gameObject.name);
            }
            else if (hite.transform.gameObject.name.Equals("Claustrofobia") && letras)
            {
                panelLetras.SetActive(true);
                Invoke("DesactivarLetras", 2f);

                //A PARTIR DE AQUI TIENE QUE EMPEZAR A SONAR EL LATIDO DEL CORAZON CADA VEZ MAS FUERTE
            }
        }
        else
        {
            GameObject.Find("Techo").GetComponent<MeshRenderer>().material.color = Color.red;
            //hite.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            //gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    public void DesactivarLetras()
    {
        panelLetras.SetActive(false);
        letras = false;
    }

    public bool ComprobarObjeto()
    {
        RaycastHit hit;

        Ray ray = new Ray(transform.position, transform.up);
        Debug.DrawRay(ray.origin, ray.direction * 30f, Color.green);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.name.Equals("Claustrofobia"))
            {
                return true;
            }
        }
        return false;
    }
}
