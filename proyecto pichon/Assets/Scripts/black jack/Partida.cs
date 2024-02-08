using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Partida : MonoBehaviour
{
    public List<GameObject> manoJugador;
    public List<GameObject> manoDealer;

    public GameObject[] paredes;

    public AlmacenCartas almacen;

    public int puntosJugador;
    public int puntosDealer;

    public float speedCartas;
    public float speedParedes;
    public float angulo;
    public float posicion;

    public float zJugador;
    public float zDealer;

    public float mov, y;

    //BOTONES
    public GameObject otraCarta;
    public GameObject plantarse;

    private void Start()
    {
        almacen.mazo = new List<(int, string, GameObject)>();
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
            manoJugador[i].transform.position = Vector3.Lerp(manoJugador[i].transform.position, new Vector3(i * posicion, manoJugador[i].transform.position.y, zJugador), speedCartas);
            manoJugador[i].transform.rotation = Quaternion.Lerp(manoJugador[i].transform.rotation, new Quaternion(manoJugador[i].transform.rotation.x, manoJugador[i].transform.rotation.y, angulo, manoJugador[i].transform.rotation.w), speedCartas);
        }

        for (int i = 0; i < manoDealer.Count; i++)
        {
            manoDealer[i].transform.position = Vector3.Lerp(manoDealer[i].transform.position, new Vector3(i * posicion, manoDealer[i].transform.position.y, zDealer), speedCartas);
            manoDealer[i].transform.rotation = Quaternion.Lerp(manoDealer[i].transform.rotation, new Quaternion(manoDealer[i].transform.rotation.x, manoDealer[i].transform.rotation.y, angulo, manoDealer[i].transform.rotation.w), speedCartas);

        }

        paredes[0].transform.position = Vector3.Lerp(paredes[0].transform.position, new Vector3(0, y, -mov), speedParedes);
        paredes[1].transform.position = Vector3.Lerp(paredes[1].transform.position, new Vector3(-mov, y, 0), speedParedes);
        paredes[2].transform.position = Vector3.Lerp(paredes[2].transform.position, new Vector3(mov, y, 0), speedParedes);
        paredes[3].transform.position = Vector3.Lerp(paredes[3].transform.position, new Vector3(0, y, mov), speedParedes);

        //paredes[4].transform.position = Vector3.Lerp(paredes[4].transform.position, new Vector3(0, paredes[4].transform.position.y, -mov), speedParedes);
        //paredes[5].transform.position = Vector3.Lerp(paredes[5].transform.position, new Vector3(0, paredes[5].transform.position.y, -mov), speedParedes);
        //paredes[6].transform.position = Vector3.Lerp(paredes[6].transform.position, new Vector3(0, paredes[6].transform.position.y, -mov), speedParedes);
        //paredes[7].transform.position = Vector3.Lerp(paredes[7].transform.position, new Vector3(0, paredes[7].transform.position.y, -mov), speedParedes);

        /*foreach (var carta in manoJugador)
        {
            carta.transform.position = Vector3.Lerp(carta.transform.position, new Vector3(carta.transform.position.x * posicion * 0.1f, carta.transform.position.y, 1f), speed);
            carta.transform.rotation = Quaternion.Lerp(carta.transform.rotation, new Quaternion(carta.transform.rotation.x, carta.transform.rotation.y, angulo, carta.transform.rotation.w), speed);
        }*/
        //manoJugador[manoJugador.Count - 1].transform.position = Vector3.Lerp(manoJugador[manoJugador.Count - 1].transform.position, new Vector3(0f, manoJugador[manoJugador.Count - 1].transform.position.y, 1f), speed);
    }

    public void IniciarRonda()
    {
        Debug.Log("-------NUEVA RONDA-------");
        //REPARTIMOS LAS CARTAS INICIALES TANTO AL DEALER COMO AL JUGADOR
        AñadirCarta(manoJugador);
        AñadirCarta(manoJugador);

        AñadirCarta(manoDealer);
        //ESTA CARTA ES BOCA ABAJO
        AñadirCarta(manoDealer);

        //CONTAMOS LOS PUNTOS QUE TIENE EL JUGADOR Y LE PREGUNTAMOS SI QUIERE COGER OTRA CARTA O PLANTARSE
        ComprobarPuntosJugador();

        //*********************************************//
        //ESTO A LO MEJOR SE PUEDE QUITAR MAS A DELANTE//
        //*********************************************//
        ComprobarPuntosDealer();
    }

    public void AñadirCarta(List<GameObject> mano)
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
            AñadirCarta(manoDealer);
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

        //HACER MAS PEQUEÑA O MAS GRANDE LA HABITACION DEPENDIENDO DE SI EL JUGADOR GANA O PIERDE
        //INICIAR SIGUIENTE RONDA

        if (jugadorGana)
        {
            Debug.Log("Gana el jugador");
            mov += 0.66f;
        }
        else
        {
            Debug.Log("Gana el dealer");
            mov -= 0.33f;
        }
        Debug.Log("Puntos jugador: " + puntosJugador + " Puntos dealer: " + puntosDealer);


        //HACER LA ANIMACION DE CAMBIO DE ESCENA, MOSTRANDO LA CONVERSACION PERTINENTE
        if (mov >= 4.4 || mov <= 1.4)
            SceneManager.LoadScene("Plato");
        else
            IniciarRonda();
    }

    public void OtraCarta()
    {
        otraCarta.SetActive(false);
        plantarse.SetActive(false);

        //DARLE OTRA CARTA AL JUGADOR Y COMPROBAR PUNTOS
        AñadirCarta(manoJugador);
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
