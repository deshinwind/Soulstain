using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit;

public class ControladorPasillo : MonoBehaviour
{
    public GameObject video;
    public GameObject bate;
    public GameObject camara2;
    public GameObject ciegoUI;

    public GameObject panelLetras;
    public GameObject panelSolido;
    public GameObject panelDialogos;

    public GameObject puerta;

    public GameObject player;

    public TMP_Text letras;

    public DialogueController dialogueController;

    public int puntos;

    public float wait = 1f;

    private bool videoCasteado = false;
    private bool letrasMostradas = false;
    private bool dialogoSegundaCamaraMostrado = false;
    private bool dialogoCercaPuertaMostrado = false;
    private bool dialogoVictoriaMostrado = false;
    private bool dialogoFinalMostrado = false;

    public bool cercaDePuerta = false;
    public bool abrirPuerta = false;
    public bool panelBlanco = false;

    public Vector3 alphaPanel = new Vector3(0, 0, 0);
    public Vector3 rotacion = new Vector3(0, -133, 0);

    public List<string> dialogoInicial = new List<string>();
    public List<string> dialogoSegundaCamara = new List<string>();
    public List<string> dialogoCercaDeLaPuerta = new List<string>();

    public List<string> dialogoVictoria = new List<string>();

    public List<string> dialogoFinal = new List<string>();

    void Update()
    {
        if (video.GetComponent<VideoPlayer>().isPlaying)
        {
            videoCasteado = true;
            
        }
        if (videoCasteado && !video.GetComponent<VideoPlayer>().isPlaying)
        {
            videoCasteado = false;
            video.SetActive(false);
        }

        if (bate.GetComponent<Bate>().isgrab && !letrasMostradas)
        {
            letrasMostradas = true;
            panelLetras.SetActive(true);
            Invoke("DesactivarLetras", 3f);
            Invoke("IniciarDialogos", 4f);
            Invoke("MostrarLetras", 6f);
        }

        if (dialogueController.pause && camara2.GetComponent<CamaraFlash>().camaraActiva && !dialogoSegundaCamaraMostrado)
        {
            dialogoSegundaCamaraMostrado = true;
            dialogueController.StartDialogue(dialogoSegundaCamara, wait);
        }
        else if (dialogueController.pause && cercaDePuerta && !dialogoCercaPuertaMostrado)
        {
            dialogoCercaPuertaMostrado = true;
            dialogueController.StartDialogue(dialogoCercaDeLaPuerta, wait);
        }
        else if (dialogueController.pause && panelBlanco && !dialogoVictoriaMostrado)
        {
            dialogoVictoriaMostrado = true;
            dialogueController.StartDialogue(dialogoVictoria, wait);
        }
        else if ((puntos >= 7 && !dialogoFinalMostrado) || (dialogoVictoriaMostrado && dialogueController.pause && !dialogoFinalMostrado))
        {
            dialogueController.pause = true;
            dialogoFinalMostrado = true;
            dialogueController.StartDialogue(dialogoFinal, wait);
        }
    }

    private void LateUpdate()
    {
        if (abrirPuerta && !panelBlanco)
        {
            puerta.transform.rotation = Quaternion.Euler(Vector3.Lerp(puerta.transform.rotation.eulerAngles, rotacion, 0.0025f));
            alphaPanel = Vector3.Lerp(alphaPanel, new Vector3(1f, 0f, 0f), 0.05f);
            panelSolido.GetComponent<Image>().color = new Color(panelSolido.GetComponent<Image>().color.r, panelSolido.GetComponent<Image>().color.g, panelSolido.GetComponent<Image>().color.b, alphaPanel.x);

            Invoke("PanelBlanco", 3f);
        }
    }

    public void PanelBlanco()
    {
        panelBlanco = true;
        panelSolido.GetComponent<Image>().color = new Color(panelSolido.GetComponent<Image>().color.r, panelSolido.GetComponent<Image>().color.g, panelSolido.GetComponent<Image>().color.b, 1f);

        player.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
        player.GetComponent<ActionBasedContinuousTurnProvider>().enabled = false;
    }

    public void AbrirPuerta()
    {
        panelSolido.gameObject.SetActive(true);
        panelDialogos.gameObject.SetActive(false);
        panelSolido.GetComponent<Image>().color = Color.white;
        abrirPuerta = true;
    }

        public void DesactivarLetras()
    {
        panelLetras.SetActive(false);
    }

    public void IniciarDialogos()
    {
        dialogueController.StartDialogue(dialogoInicial, wait);
    }

    public void MostrarLetras()
    {
        letras.text = "MULTITUD";
        panelLetras.SetActive(true);
        Invoke("IniciarDialogos", 3f);
    }

    public void sumarpuntos()
    {
        puntos++;
        ciegoUI.SetActive(true);
    }
}
