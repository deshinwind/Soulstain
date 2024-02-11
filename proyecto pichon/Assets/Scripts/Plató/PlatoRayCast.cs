using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlatoRayCast : MonoBehaviour
{
    /*public PlatoDialogos platoDialogos;

    public GameObject panelLetras;
    public GameObject claustrofobia;

    public GameObject techo;

    public GameObject panelSolido;

    public Vector3 alphaPanel = new Vector3(0f, 0f, 0f);

    public float speed;

    private bool letras = true;

    public AudioClip latido;
    public bool latiendo = false;
    public bool salaPequeña = false;
    */
    public RaycastHit hit;
    public Ray ray;
    
    /*private void Update()
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
    }*/


    public void CastRay()
    {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 30f, Color.green);

        //Physics.Raycast(ray, out hit);
    }

    /*public void PrimeraComprobacion()
    {
        //QUITAR COMENTARIO CUANDO PONGA AUDIO
        /*if (latiendo)
        {
            latido.volume += 0.1f;
        }

        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 30f, Color.green);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.name.Equals("Techo"))
            {
                if (platoDialogos.pause && !claustrofobia.activeSelf)
                {
                    hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    platoDialogos.pause = false;
                    claustrofobia.SetActive(true);

                    //EMPIEZA A HACERSE LA SALA PEQUEÑA POCO A POCO
                    salaPequeña = true;
                }
                Debug.Log(hit.transform.gameObject.name);
            }
            else if (hit.transform.gameObject.name.Equals("Claustrofobia") && letras)
            {
                panelLetras.SetActive(true);
                Invoke("DesactivarLetras", 2f);

                //QUITAR COMENTARIO CUANDO PONGA AUDIO
                //A PARTIR DE AQUI TIENE QUE EMPEZAR A SONAR EL LATIDO DEL CORAZON CADA VEZ MAS FUERTE
                //latiendo = true;
                //latido.Play();
            }
        }
        else
        {
            GameObject.Find("Techo").GetComponent<MeshRenderer>().material.color = Color.red;
            //hite.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            //gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        if (salaPequeña && claustrofobia.activeSelf)
        {
            techo.transform.position = Vector3.Lerp(techo.transform.position, new Vector3(techo.transform.position.x, 3f, techo.transform.position.z), speed);
        }

        if (platoDialogos.pause && claustrofobia.activeSelf)
        {
            if (!panelSolido.activeSelf)
            {
                panelSolido.GetComponent<Image>().color = Color.black;
                panelSolido.gameObject.SetActive(true);
            }
            Debug.Log("Esyou fentypppp");
            alphaPanel = Vector3.Lerp(alphaPanel, new Vector3(1f, 0f, 0f), 0.01f);
            panelSolido.GetComponent<Image>().color = new Color(panelSolido.GetComponent<Image>().color.r, panelSolido.GetComponent<Image>().color.g, panelSolido.GetComponent<Image>().color.b, alphaPanel.x);

            Invoke("PrimeraTransicion", 5f);
        }
        /*RaycastHit hit;

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
    }*/

    /*public void SegundaComprobacion()
    {
        
    }

    public void TerceraComprobacion()
    {
        
    }

    public void DesactivarLetras()
    {
        panelLetras.SetActive(false);
        letras = false;
    }

    public void PrimeraTransicion()
    {

        SceneManager.LoadScene("BlackJack");
    }*/
}
