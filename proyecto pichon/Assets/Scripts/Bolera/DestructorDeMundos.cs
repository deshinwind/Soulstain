using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestructorDeMundos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bola") || other.CompareTag("Bolo"))
        {
            //Debug.Log(other.gameObject.name);
            Destroy(other.gameObject);
        }
    }
}
