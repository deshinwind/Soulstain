using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmacenCartas : MonoBehaviour
{
    public GameObject prefabCarta;

    public List<(int, string, Material)> mazo;

    public List<int> valor;
    public List<string> palo;
    public List<Material> material;

    private int numero;

    /*private void Start()
    {
        mazo = new List<(int, string, Material)>();

        LlenarMazo();
    }*/

    private void Update()
    {
        if (mazo.Count == 0)
        {
            mazo.Clear();
            LlenarMazo();
        }
    }

    public void LlenarMazo()
    {
        for (int i = 0; i < 52; i++)
        {
            mazo.Add((valor[i % 13], palo[i / 13], material[i]));
        }
    }

    public GameObject CrearCarta()
    {
        numero = Random.Range(0, mazo.Count - 1);

        GameObject cartaNueva = InstanciarCarta();

        mazo.RemoveAt(numero);

        //HACER LA ANIMACION PARA QUE LA CARTA VAYA DESDE EL PUN TO EN EL QUE SE HA CREADO HASTA LA POSICION QUE LE CORRESPONDE
        //DEPENDIENDO DE SI LA CARTA ES PARA EL DEALER O PARA EL JUGADOR Y DEPENDIENDO DEL NUMERO DE CARTAS QUE TENGA EN LA MANO

        return cartaNueva;
    }

    public GameObject InstanciarCarta()
    {
        GameObject cartaNueva = Instantiate(prefabCarta);

        cartaNueva.transform.position = prefabCarta.transform.position;
        cartaNueva.transform.rotation = prefabCarta.transform.rotation;
        cartaNueva.transform.localScale = prefabCarta.transform.localScale;

        cartaNueva.GetComponent<Carta>().valor = mazo[numero].Item1;
        cartaNueva.GetComponent<Carta>().palo = mazo[numero].Item2;
        cartaNueva.GetComponent<MeshRenderer>().material = mazo[numero].Item3;

        return cartaNueva;
    }
}
