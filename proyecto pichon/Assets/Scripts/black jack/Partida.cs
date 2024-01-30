using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Partida : MonoBehaviour
{
    public List<GameObject> manoJugador;
    public List<GameObject> manoDealer;

    public AlmacenCartas almacen;

    public int puntosJugador;
    public int puntosDealer;

    public float speed;
    public float angulo;
    public float posicion;

    //BOTONES
    public GameObject otraCarta;
    public GameObject plantarse;

    private void Start()
    {
        almacen.mazo = new List<(int, string, Material)>();
        almacen.LlenarMazo();

        manoJugador = new List<GameObject>();
        manoDealer = new List<GameObject>();

        IniciarRonda();
    }

    private void LateUpdate()
    {
        //EL NUMERO MAXIMO DE CARTAS QUE PUEDE TENER UNA PERSONA ES DE 12 (4 AS, 4 2 Y 3 3)
        for (int i = 0; i < manoJugador.Count; i++)
        {
            manoJugador[i].transform.position = Vector3.Lerp(manoJugador[i].transform.position, new Vector3(i * posicion, manoJugador[i].transform.position.y, 1f), speed);
            manoJugador[i].transform.rotation = Quaternion.Lerp(manoJugador[i].transform.rotation, new Quaternion(manoJugador[i].transform.rotation.x, manoJugador[i].transform.rotation.y, angulo, manoJugador[i].transform.rotation.w), speed);
        }

        for (int i = 0; i < manoDealer.Count; i++)
        {
            manoDealer[i].transform.position = Vector3.Lerp(manoDealer[i].transform.position, new Vector3(i * posicion, manoDealer[i].transform.position.y, 1f), speed);
            manoDealer[i].transform.rotation = Quaternion.Lerp(manoDealer[i].transform.rotation, new Quaternion(manoDealer[i].transform.rotation.x, manoDealer[i].transform.rotation.y, angulo, manoDealer[i].transform.rotation.w), speed);

        }
        /*foreach (var carta in manoJugador)
        {
            carta.transform.position = Vector3.Lerp(carta.transform.position, new Vector3(carta.transform.position.x * posicion * 0.1f, carta.transform.position.y, 1f), speed);
            carta.transform.rotation = Quaternion.Lerp(carta.transform.rotation, new Quaternion(carta.transform.rotation.x, carta.transform.rotation.y, angulo, carta.transform.rotation.w), speed);
        }*/
        //manoJugador[manoJugador.Count - 1].transform.position = Vector3.Lerp(manoJugador[manoJugador.Count - 1].transform.position, new Vector3(0f, manoJugador[manoJugador.Count - 1].transform.position.y, 1f), speed);
    }

    public void IniciarRonda()
    {
        //REPARTIMOS LAS CARTAS INICIALES TANTO AL DEALER COMO AL JUGADOR
        A�adirCarta(manoJugador);
        A�adirCarta(manoJugador);

        A�adirCarta(manoDealer);
        //ESTA CARTA ES BOCA ABAJO
        A�adirCarta(manoDealer);

        //LE PREGUNTAMOS AL JUGADOR SI DESEA ROBAR UNA CARTA O PLANTARSE
        otraCarta.SetActive(true);
        plantarse.SetActive(true);
    }

    public void A�adirCarta(List<GameObject> mano)
    {
        mano.Add(almacen.CrearCarta());
    }

    public void ComprobarPuntosDealer()
    {
        //*********************************************//
        //HAY QUE TENER EN CUENTA QUE EL AS VALE 1 O 11//
        //*********************************************//
        puntosDealer = 0;

        foreach (var carta in manoDealer)
        {
            puntosDealer += carta.GetComponent<Carta>().valor;
        }
    }

    public void ComprobarPuntosJugador()
    {
        //*********************************************//
        //HAY QUE TENER EN CUENTA QUE EL AS VALE 1 O 11//
        //*********************************************//
        puntosJugador = 0;

        foreach (var carta in manoJugador)
        {
            puntosJugador += carta.GetComponent<Carta>().valor;
        }

        Debug.Log("Puntos actuales Jugador: " + puntosJugador);

        if (puntosJugador < 21)
        {
            //PREGUNTAR SI QUIERE OTRA CARTA O SI SE QUIERE PLANTAR
            otraCarta.SetActive(true);
            plantarse.SetActive(true);
        }
        else if (puntosJugador == 21)
        {
            //JUGADOR GANA LA RONDA
            FinDeRonda(true);
        }
        else
        {
            //JUGADOR PIERDE LA RONDA
            FinDeRonda(false);
        }
    }

    public void Plantarse()
    {
        if (otraCarta.activeSelf)
        {
            otraCarta.SetActive(false);
            plantarse.SetActive(false);
        }

        //LEVANTAR LA SEGUNDA CARTA DEL DEALER PARA QUE VEA
        //COMPROBAR LOS PUNTOS DEL JUGADOR. SI NO LE DOY A ROBAR CARTA EN NUNGUN MOMENTO NO CALCULO LOS PUNTOS
        ComprobarPuntosDealer();

        if (puntosDealer < 17)
        {
            //EL DEALER ROBA OTRA CARTA
            A�adirCarta(manoDealer);
            Plantarse();
        }
        else if (puntosDealer > 21)
        {
            //GANA EL JUGADOR
            FinDeRonda(true);
        }
        else
        {
            //SE COMPARAN LOS PUNTOS PARA VER QUIEN GANA
            if (puntosDealer > puntosJugador)
            {
                //GANA EL DEALER
                FinDeRonda(false);
            }
            else
            {
                //GANA EL JUGADOR
                FinDeRonda(true);
            }
        }
    }

    public void FinDeRonda(bool jugadorGana)
    {
        VaciarMano(manoDealer);
        VaciarMano(manoJugador);

        //HACER MAS PEQUE�A O MAS GRANDE LA HABITACION DEPENDIENDO DE SI EL JUGADOR GANA O PIERDE
        //INICIAR SIGUIENTE RONDA

        if (jugadorGana)
        {
            Debug.Log("Gana el jugador");
        }
        else
        {
            Debug.Log("Gana el dealer");
        }
        Debug.Log("Puntos jugador: " + puntosJugador + " Puntos dealer: " + puntosDealer);

        IniciarRonda();
    }

    public void OtraCarta()
    {
        otraCarta.SetActive(false);
        plantarse.SetActive(false);

        //DARLE OTRA CARTA AL JUGADOR Y COMPROBAR PUNTOS
        A�adirCarta(manoJugador);
        ComprobarPuntosJugador();
    }

    public void VaciarMano(List<GameObject> mano)
    {
        //DESTRUIMOS LAS CARTAS ANTES DE VACIAR LAS MANOS
        foreach (var carta in mano)
        {
            Destroy(carta);
        }

        mano.Clear();
    }
}
