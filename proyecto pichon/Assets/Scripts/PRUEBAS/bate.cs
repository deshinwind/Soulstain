using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bate : MonoBehaviour
{
    public Material mesatext;
    public bool isgrab = false;
    public GameObject mesarota;
    public void agarrao()
    {
        isgrab = true;
    }

    public void noagarrao()
    {
        isgrab=false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "mesa" && isgrab)
        {
            mesarota = Instantiate(mesarota, collision.transform.position, mesarota.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(mesarota, 3f);
        }
    }

}
