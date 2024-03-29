using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Partida : MonoBehaviour
{
    public List<GameObject> manoJugador;
    public List<GameObject> manoDealer;

    public GameObject[] paredes;

    public AlmacenCartas almacen;

    public DialogueController dialogueController;

    public ControladorBJ controladroBJ;

    public int puntosJugador;
    public int puntosDealer;
    public int numeroRonda = 0;

    public float speedCartas;
    public float speedParedes;
    public float angulo;
    public float anguloAUX;
    public float posicion;

    public float zJugador;
    public float zDealer;

    public float mov, y;

    public float timer;
    public float giroDealer;
    public float giroJugador;

    public bool leToca;
    public bool jugadorGana;
    public bool desvelar;
    public bool primeraVez = true;

    public bool rondaEnJuego = false;

    public bool finDeRonda = false;

    public Vector3 rotacion;

    public AudioClip clipCarta;

    //BOTONES DEFINITIVOS
    public GameObject botonOtra;
    public GameObject botonPlantarse;

    private void Start()
    {
        almacen.mazo = new List<(int, string, GameObject)>();
        almacen.LlenarMazo();

        manoJugador = new List<GameObject>();
        manoDealer = new List<GameObject>();

        rotacion = almacen.mazo[0].Item3.transform.rotation.eulerAngles;

        //IniciarRonda();
    }

    private void Update()
    {
        if (rondaEnJuego)
        {
            if (timer >= 1f)
            {
                timer = 0;
            }

            if (timer == 0 && giroJugador < manoJugador.Count)
            {
                dialogueController.audioCartasYFoco.clip = clipCarta;
                dialogueController.audioCartasYFoco.Play();
                giroJugador++;
            }
            if (timer == 0 && giroJugador == manoJugador.Count && giroDealer < manoDealer.Count)
            {
                if (leToca)
                {
                    dialogueController.audioCartasYFoco.clip = clipCarta;
                    dialogueController.audioCartasYFoco.Play();
                    giroDealer++;
                }
                else
                    leToca = true;
            }

            timer += Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (finDeRonda)
        {
            foreach (GameObject carta in GameObject.FindGameObjectsWithTag("Carta"))
            {
                carta.transform.position = Vector3.Lerp(carta.transform.position, new Vector3(0.8f, 1.0427f, -0.5303f), speedCartas);
            }
        }

        //EL NUMERO MAXIMO DE CARTAS QUE PUEDE TENER UNA PERSONA ES DE 12 (4 AS, 4 2 Y 3 3)
        if (rondaEnJuego)
        {
            if (manoJugador.Count != 0)
            {
                for (int i = 0; i < giroJugador; i++)
                {
                    manoJugador[i].transform.position = Vector3.Lerp(manoJugador[i].transform.position, new Vector3(i * posicion - 0.3f, manoJugador[i].transform.position.y, zJugador), speedCartas);

                    manoJugador[i].transform.rotation = Quaternion.Euler(Vector3.Lerp(rotacion, new Vector3(rotacion.x, rotacion.y, angulo), speedCartas));

                    //manoJugador[i].transform.rotation = Quaternion.Lerp(manoJugador[i].transform.rotation, new Quaternion(manoJugador[i].transform.rotation.x, manoJugador[i].transform.rotation.y, angulo, manoJugador[i].transform.rotation.w), speedCartas);
                }

                for (int i = 0; i < giroDealer; i++)
                {
                    manoDealer[i].transform.position = Vector3.Lerp(manoDealer[i].transform.position, new Vector3(i * posicion, manoDealer[i].transform.position.y, zDealer), speedCartas);

                    if (i == 1)
                    {
                        if (desvelar)
                        {
                            manoDealer[i].transform.rotation = Quaternion.Euler(Vector3.Lerp(rotacion, new Vector3(rotacion.x, rotacion.y, angulo), speedCartas));
                        }
                        else
                        {
                            manoDealer[i].transform.rotation = Quaternion.Euler(Vector3.Lerp(rotacion, new Vector3(rotacion.x, rotacion.y, anguloAUX), speedCartas));
                        }
                    }
                    else
                    {
                        manoDealer[i].transform.rotation = Quaternion.Euler(Vector3.Lerp(rotacion, new Vector3(rotacion.x, rotacion.y, angulo), speedCartas));
                    }

                    //manoDealer[i].transform.rotation = Quaternion.Lerp(manoDealer[i].transform.rotation, new Quaternion(manoDealer[i].transform.rotation.x, manoDealer[i].transform.rotation.y, angulo, manoDealer[i].transform.rotation.w), speedCartas);
                }
            }

            paredes[0].transform.position = Vector3.Lerp(paredes[0].transform.position, new Vector3(0, y, mov), speedParedes);
            paredes[1].transform.position = Vector3.Lerp(paredes[1].transform.position, new Vector3(-mov, y, 0), speedParedes);
            paredes[2].transform.position = Vector3.Lerp(paredes[2].transform.position, new Vector3(mov, y, 0), speedParedes);
            paredes[3].transform.position = Vector3.Lerp(paredes[3].transform.position, new Vector3(0, y, -mov), speedParedes);

            /*foreach (var carta in manoJugador)
            {
                carta.transform.position = Vector3.Lerp(carta.transform.position, new Vector3(carta.transform.position.x * posicion * 0.1f, carta.transform.position.y, 1f), speed);
                carta.transform.rotation = Quaternion.Lerp(carta.transform.rotation, new Quaternion(carta.transform.rotation.x, carta.transform.rotation.y, angulo, carta.transform.rotation.w), speed);
            }*/
            //manoJugador[manoJugador.Count - 1].transform.position = Vector3.Lerp(manoJugador[manoJugador.Count - 1].transform.position, new Vector3(0f, manoJugador[manoJugador.Count - 1].transform.position.y, 1f), speed);
        }
    }

    public void IniciarRonda()
    {
        rondaEnJuego = true;
        desvelar = false;

        Debug.Log("-------NUEVA RONDA-------");
        //REPARTIMOS LAS CARTAS INICIALES TANTO AL DEALER COMO AL JUGADOR
        AņadirCarta(manoJugador);
        AņadirCarta(manoJugador);

        AņadirCarta(manoDealer);
        //ESTA CARTA ES BOCA ABAJO
        AņadirCarta(manoDealer);

        //CONTAMOS LOS PUNTOS QUE TIENE EL JUGADOR Y LE PREGUNTAMOS SI QUIERE COGER OTRA CARTA O PLANTARSE
        ComprobarPuntosJugador();

        //*********************************************//
        //ESTO A LO MEJOR SE PUEDE QUITAR MAS A DELANTE//
        //*********************************************//
        ComprobarPuntosDealer();
    }

    public void AņadirCarta(List<GameObject> mano)
    {
        mano.Add(almacen.CrearCarta());
    }

    public void ComprobarPuntosDealer()
    {
        //*********************************************//
        //HAY QUE TENER EN CUENTA QUE EL AS VALE 1 O 11//
        //*********************************************//
        puntosDealer = 0;

        foreach (GameObject carta in manoDealer)
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

        foreach (GameObject carta in manoJugador)
        {
            puntosJugador += carta.GetComponent<Carta>().valor;
        }

        Debug.Log("Puntos actuales Jugador: " + puntosJugador);

        if (puntosJugador < 21)
        {
            //PREGUNTAR SI QUIERE OTRA CARTA O SI SE QUIERE PLANTAR
            Invoke("ActivarBotones", 3f);
        }
        else if (puntosJugador == 21)
        {
            //JUGADOR GANA LA RONDA
            jugadorGana = true;
            if (numeroRonda != 0)
                controladroBJ.victorias++;
            Invoke("TamaņoParedes", 5f);
            Invoke("FinDeRonda", 7f);
        }
        else
        {
            //JUGADOR PIERDE LA RONDA
            jugadorGana = false;
            if (numeroRonda != 0)
                controladroBJ.derrotas++;
            Invoke("TamaņoParedes", 5f);
            Invoke("FinDeRonda", 7f);
        }
    }

    public void ActivarBotones()
    {
        botonOtra.GetComponent<XRSimpleInteractable>().enabled = true;
        botonPlantarse.GetComponent<XRSimpleInteractable>().enabled = true;
    }

    public void Plantarse()
    {
        desvelar = true;

        if (botonOtra.GetComponent<XRSimpleInteractable>().enabled)
        {
            botonOtra.GetComponent<XRSimpleInteractable>().enabled = false;
            botonPlantarse.GetComponent<XRSimpleInteractable>().enabled = false;
        }

        //LEVANTAR LA SEGUNDA CARTA DEL DEALER PARA QUE VEA
        //COMPROBAR LOS PUNTOS DEL JUGADOR. SI NO LE DOY A ROBAR CARTA EN NUNGUN MOMENTO NO CALCULO LOS PUNTOS
        ComprobarPuntosDealer();

        if (puntosDealer < 17)
        {
            //EL DEALER ROBA OTRA CARTA
            AņadirCarta(manoDealer);
            Plantarse();
        }
        else if (puntosDealer > 21)
        {
            //GANA EL JUGADOR
            jugadorGana = true;
            if (numeroRonda != 0)
                controladroBJ.victorias++;
            Invoke("TamaņoParedes", 5f);
            Invoke("FinDeRonda", 7f);
        }
        else
        {
            //SE COMPARAN LOS PUNTOS PARA VER QUIEN GANA
            
            if (puntosDealer > puntosJugador)
            {
                //GANA EL DEALER
                jugadorGana = false;
                if (numeroRonda != 0)
                    controladroBJ.derrotas++;
                Invoke("TamaņoParedes", 5f);
                Invoke("FinDeRonda", 7f);
            }
            else
            {
                //GANA EL JUGADOR
                jugadorGana = true;
                if (numeroRonda != 0)
                    controladroBJ.victorias++;
                Invoke("TamaņoParedes", 5f);
                Invoke("FinDeRonda", 7f);
            }
        }
    }

    public void TamaņoParedes()
    {
        Debug.Log("Victorias: " + controladroBJ.victorias + " Derrotas: " + controladroBJ.derrotas);
        if (numeroRonda != 0)
        {
            if (jugadorGana)
            {
                switch (controladroBJ.victorias)
                {
                    case 1:
                        mov = 1.2f;
                        break;
                    case 2:
                        mov = 0.6f;
                        break;
                    case 3:
                        mov = 0.0f;
                        break;
                }
                Debug.Log("Gana el jugador");
                //mov -= 0.8f;
            }
            else
            {
                switch (controladroBJ.derrotas)
                {
                    case 1:
                        mov = 1.9f;
                        break;
                    case 2:
                        mov = 2.4f;
                        break;
                    case 3:
                        mov = 2.9f;
                        break;
                }
                Debug.Log("Gana el dealer");
                //mov += 0.4f;
            }
            Debug.Log(mov);
        }
    }

    public void FinDeRonda()
    {
        finDeRonda = true;
        rondaEnJuego = false;
        giroJugador = 0;
        giroDealer = 0;
        leToca = false;
        numeroRonda++;

        Invoke("VaciarMano", 2f);

        Debug.Log("Puntos jugador: " + puntosJugador + " Puntos dealer: " + puntosDealer);

        controladroBJ.iniciarRonda = true;
    }

    public void OtraCarta()
    {
        botonOtra.GetComponent<XRSimpleInteractable>().enabled = false;
        botonPlantarse.GetComponent<XRSimpleInteractable>().enabled = false;

        //DARLE OTRA CARTA AL JUGADOR Y COMPROBAR PUNTOS
        AņadirCarta(manoJugador);
        ComprobarPuntosJugador();
    }

    public void VaciarMano()
    {
        finDeRonda = false;

        //DESTRUIMOS LAS CARTAS ANTES DE VACIAR LAS MANOS
        foreach (GameObject carta in manoDealer)
        {
            Destroy(carta);
        }

        foreach (GameObject carta in manoJugador)
        {
            Destroy(carta);
        }

        manoDealer.Clear();
        manoJugador.Clear();
    }
}
