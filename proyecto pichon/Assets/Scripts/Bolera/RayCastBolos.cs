using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastBolos : MonoBehaviour
{
    private void Update()
    {
        //RaycastHit hite;

        Ray raye = new Ray(transform.position, transform.up);
        Debug.DrawRay(raye.origin, raye.direction * 30f, Color.green);

        /*if (Physics.Raycast(raye, out hite))
        {
            hite.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            Debug.Log(hite.transform.gameObject.name);
        }*/
    }

    public bool ComprobarBolo()
    {
        RaycastHit hit;

        Ray ray = new Ray(transform.position, transform.up);
        Debug.DrawRay(ray.origin, ray.direction * 30f, Color.green);

        if(Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.name.Equals("Cielo"))
            {
                return true;
            }
        }
        return false;
    }
}
