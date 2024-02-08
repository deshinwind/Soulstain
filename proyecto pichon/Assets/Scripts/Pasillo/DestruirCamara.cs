using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DestruirCamara : MonoBehaviour
{
    public Transform Bate;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, player.transform.forward); AGUSTAR EL GIRO DE LA CAMARA PARA QUE SIGA AL PERSONAJE
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("mesa"))
        {
            if (Bate.GetComponent<XRGrabInteractable>().isSelected == true)
            {

            }
        }
        Debug.Log("FUNCIONAS O QUE?");
        if (collision.gameObject.name.Equals("bate"))
        {
            //Instantiate(Object1, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
