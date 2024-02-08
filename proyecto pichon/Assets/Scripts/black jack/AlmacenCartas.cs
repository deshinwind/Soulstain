using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmacenCartas : MonoBehaviour
{
    //public GameObject prefabCarta;

    public List<(int, string, GameObject)> mazo;

    public List<int> valor;
    public List<string> palo;
    public List<GameObject> prefabCartas;

    private int numero;

    /*private void Start()
    {
        mazo = new List<(int, string, Material)>();

        LlenarMazo();
    }*/

    public void LlenarMazo()
    {
        for (int i = 0; i < 52; i++)
        {
            mazo.Add((valor[i % 13], palo[i / 13], prefabCartas[i]));
        }
    }

    public GameObject CrearCarta()
    {
        numero = Random.Range(0, mazo.Count - 1);

        GameObject cartaNueva = InstanciarCarta();

        mazo.RemoveAt(numero);

        //HACER LA ANIMACION PARA QUE LA CARTA VAYA DESDE EL PUNTO EN EL QUE SE HA CREADO HASTA LA POSICION QUE LE CORRESPONDE
        //DEPENDIENDO DE SI LA CARTA ES PARA EL DEALER O PARA EL JUGADOR Y DEPENDIENDO DEL NUMERO DE CARTAS QUE TENGA EN LA MANO

        return cartaNueva;
    }

    public GameObject InstanciarCarta()
    {

        //COMPROBAMOS SI QUEDAN CARCAS EN EL MAZO. EN CASO DE NO QUEDAR CARTAS LLENAMOS EL MAZO
        if (mazo.Count == 0)
        {
            mazo.Clear();
            LlenarMazo();
        }

        GameObject cartaNueva = Instantiate(mazo[numero].Item3);

        cartaNueva.transform.position = mazo[numero].Item3.transform.position;
        cartaNueva.transform.rotation = mazo[numero].Item3.transform.rotation;
        cartaNueva.transform.localScale = mazo[numero].Item3.transform.localScale;

        cartaNueva.GetComponent<Carta>().valor = mazo[numero].Item1;
        cartaNueva.GetComponent<Carta>().palo = mazo[numero].Item2;
        //cartaNueva.GetComponent<MeshRenderer>().material = mazo[numero].Item3.GetComponent<Material>();

        return cartaNueva;
    }
}
