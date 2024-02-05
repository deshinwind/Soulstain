using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlador : MonoBehaviour
{
    public int ronda;
    public int bolosTotales;
    public int bolosMaxPosibles;
    public int bolosEnPie;

    void Start()
    {
        ronda = 1;
        bolosTotales = 0;
        bolosMaxPosibles = 100;
    }

    void Update()
    {
        if (ronda > 10 || bolosMaxPosibles < 75)
        {
            //FIN DE LA PARTIDA
            SceneManager.LoadScene("Plato");
        }
    }
}
