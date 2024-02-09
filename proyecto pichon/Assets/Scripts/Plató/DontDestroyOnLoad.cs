using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public int controlador;

    public static DontDestroyOnLoad instancia;

    // Start is called before the first frame update
    void Start()
    {
        if (instancia == null)
        {
            instancia = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
