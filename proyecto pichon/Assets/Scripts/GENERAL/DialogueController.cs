using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public TMP_Text dialogText;

    public TMP_Text nombre;

    public float delay = 0.1f;

    public GameObject panelDialogo;
    public GameObject player;

    private float pauseBetween = 1f;
    private float timer;

    private List<string> dialogList = new List<string>();

    private int currentLetterIndex = 0;
    private int currentDialog = 0;

    private string currentText;

    public bool isShowingText = false;
    private bool isWaiting = false;
    public bool pause = false;

    public AudioSource audioDialogos;
    public AudioSource audioEfectos;
    public AudioSource audioCartasYFoco;

    public AudioClip[] voces;


    public void StartDialogue(List<string> dialogs, float pauseDuration)
    {
        isShowingText = true; //nuevo
        dialogList = dialogs;
        currentDialog = 0;
        pauseBetween = pauseDuration;
        pause = false;
        ShowNextDialogue();
    }

    public void ShowNextDialogue()
    {
        Debug.Log("currentDialog: " + currentDialog + " dialogList.Count: " + dialogList.Count);

        if (currentDialog < dialogList.Count)
        {
            /*if (dialogList[currentDialog].Equals(""))
            {
                encenderLuz = true;
                player.GetComponent<ContinuousMoveProviderBase>().enabled = true;
                player.GetComponent<ContinuousTurnProviderBase>().enabled = true;
                //A PARTIR DE AQUI EL JUGADOR PUEDE MOVERSE
            }*/

            currentText = dialogList[currentDialog];
            dialogText.text = "";
            currentLetterIndex = 0;
            timer = 0f;
            isShowingText = true;
            currentDialog++;
        }
        else
        {
            isShowingText = false;
            panelDialogo.SetActive(false);  //NUEVO
            //dialogText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!pause)
        {
            if (currentText != null)
            {
                if (currentText.Equals("El Ámbito") || currentText.Equals("Pelko") || currentText.Contains("Pánico escénico") || currentText.Equals("Enoclofobia")
                    || currentText.Equals("Voz Anónima") || currentText.Equals("Linterna on") || currentText.Equals("Linterna off") || currentText.Equals("Botellas")
                    || currentText.Equals("Cerradura") || currentText.Equals("Golpe") || currentText.Equals("Lluvia") || currentText.Equals("Pasos")
                    || currentText.Equals("Porrazo") || currentText.Equals("Bosque") || currentText.Equals("Ropa"))
                {
                    switch (currentText)
                    {
                        case "El Ámbito":
                            if (audioDialogos.GetComponent<AudioSource>().clip != voces[0])
                            {
                                audioDialogos.GetComponent<AudioSource>().volume = 0.15f;
                                audioDialogos.GetComponent<AudioSource>().clip = voces[0];
                            }
                            break;
                        case "Pelko":
                            if (audioDialogos.GetComponent<AudioSource>().clip != voces[1])
                            {
                                audioDialogos.GetComponent<AudioSource>().volume = 1f;
                                audioDialogos.GetComponent<AudioSource>().clip = voces[1];
                            }
                            break;
                        case "Pánico escénico A":
                            if (audioDialogos.GetComponent<AudioSource>().clip != voces[2])
                            {
                                audioDialogos.GetComponent<AudioSource>().volume = 1f;
                                audioDialogos.GetComponent<AudioSource>().clip = voces[2];
                            }
                            break;
                        case "Pánico escénico D":
                            if (audioDialogos.GetComponent<AudioSource>().clip != voces[3])
                            {
                                audioDialogos.GetComponent<AudioSource>().volume = 1f;
                                audioDialogos.GetComponent<AudioSource>().clip = voces[3];
                            }
                            break;
                        case "Pánico escénico I":
                            if (audioDialogos.GetComponent<AudioSource>().clip != voces[4])
                            {
                                audioDialogos.GetComponent<AudioSource>().volume = 1f;
                                audioDialogos.GetComponent<AudioSource>().clip = voces[4];
                            }
                            break;
                        case "Pánico escénico N":
                            if (audioDialogos.GetComponent<AudioSource>().clip != voces[5])
                            {
                                audioDialogos.GetComponent<AudioSource>().volume = 1f;
                                audioDialogos.GetComponent<AudioSource>().clip = voces[5];
                            }
                            break;
                        case "Pánico escénico T":
                            if (audioDialogos.GetComponent<AudioSource>().clip != voces[6])
                            {
                                audioDialogos.GetComponent<AudioSource>().volume = 1f;
                                audioDialogos.GetComponent<AudioSource>().clip = voces[6];
                            }
                            break;
                        case "Enoclofobia":
                            if (audioDialogos.GetComponent<AudioSource>().clip != voces[7])
                            {
                                audioDialogos.GetComponent<AudioSource>().volume = 1f;
                                audioDialogos.GetComponent<AudioSource>().clip = voces[7];
                            }
                            break;
                        case "Voz anónima":
                            if (audioDialogos.GetComponent<AudioSource>().clip != voces[8])
                            {
                                audioDialogos.GetComponent<AudioSource>().volume = 1f;
                                audioDialogos.GetComponent<AudioSource>().clip = voces[8];
                            }
                            break;
                        case "Pánico escénico P":
                            if (audioDialogos.GetComponent<AudioSource>().clip != voces[1])
                            {
                                audioDialogos.GetComponent<AudioSource>().volume = 1f;
                                audioDialogos.GetComponent<AudioSource>().clip = voces[1];
                            }
                            break;
                        case "Linterna on":
                            if (audioEfectos.GetComponent<AudioSource>().clip != voces[9])
                            {
                                audioEfectos.GetComponent<AudioSource>().volume = 1f;
                                audioEfectos.GetComponent<AudioSource>().clip = voces[9];
                                audioEfectos.Play();
                            }
                            break;
                        case "Linterna off":
                            if (audioEfectos.GetComponent<AudioSource>().clip != voces[10])
                            {
                                audioEfectos.GetComponent<AudioSource>().volume = 1f;
                                audioEfectos.GetComponent<AudioSource>().clip = voces[10];
                                audioEfectos.Play();
                            }
                            break;
                        case "Botellas":
                            if (audioEfectos.GetComponent<AudioSource>().clip != voces[11])
                            {
                                audioEfectos.GetComponent<AudioSource>().volume = 1f;
                                audioEfectos.GetComponent<AudioSource>().clip = voces[11];
                                audioEfectos.Play();
                            }
                            break;
                        case "Cerradura":
                            if (audioEfectos.GetComponent<AudioSource>().clip != voces[12])
                            {
                                audioEfectos.GetComponent<AudioSource>().volume = 1f;
                                audioEfectos.GetComponent<AudioSource>().clip = voces[12];
                                audioEfectos.Play();
                            }
                            break;
                        case "Golpe":
                            if (audioEfectos.clip != voces[13])
                            {
                                audioEfectos.volume = 1f;
                                audioEfectos.clip = voces[13];
                                audioEfectos.Play();
                            }
                            break;
                        case "Lluvia":
                            if (audioEfectos.GetComponent<AudioSource>().clip != voces[14])
                            {
                                audioEfectos.GetComponent<AudioSource>().volume = 1f;
                                audioEfectos.GetComponent<AudioSource>().clip = voces[14];
                                audioEfectos.Play();
                            }
                            break;
                        case "Pasos":
                            if (audioEfectos.GetComponent<AudioSource>().clip != voces[15])
                            {
                                audioEfectos.GetComponent<AudioSource>().volume = 1f;
                                audioEfectos.GetComponent<AudioSource>().clip = voces[15];
                                audioEfectos.Play();
                            }
                            break;
                        case "Porrazo":
                            if (audioEfectos.GetComponent<AudioSource>().clip != voces[16])
                            {
                                audioEfectos.GetComponent<AudioSource>().volume = 1f;
                                audioEfectos.GetComponent<AudioSource>().clip = voces[16];
                                audioEfectos.Play();
                            }
                            break;
                        case "Bosque":
                            if (audioEfectos.GetComponent<AudioSource>().clip != voces[17])
                            {
                                audioEfectos.GetComponent<AudioSource>().volume = 1f;
                                audioEfectos.GetComponent<AudioSource>().clip = voces[17];
                                audioEfectos.Play();
                            }
                            break;
                        case "Ropa":
                            if (audioEfectos.GetComponent<AudioSource>().clip != voces[18])
                            {
                                audioEfectos.GetComponent<AudioSource>().volume = 1f;
                                audioEfectos.GetComponent<AudioSource>().clip = voces[18];
                                audioEfectos.Play();
                            }
                            break;
                    }
                    panelDialogo.SetActive(true);
                    if (currentText.Contains("Pánico escénico"))
                    {
                        nombre.text = "Mirada";
                    }
                    else
                    {
                        nombre.text = currentText;
                    }
                    ShowNextDialogue();
                }
                else if (!currentText.Equals(""))
                {
                    panelDialogo.SetActive(true);
                    if (isShowingText)
                    {
                        timer += Time.deltaTime;
                        if (timer > delay)
                        {
                            if (!audioDialogos.GetComponent<AudioSource>().isPlaying)
                            {
                                audioDialogos.Play();
                            }
                            dialogText.text += currentText[currentLetterIndex];
                            currentLetterIndex++;
                            if (currentLetterIndex >= currentText.Length)
                            {
                                isShowingText = false;
                                isWaiting = true;
                            }
                            timer = 0;
                        }
                    }
                    else if (isWaiting)
                    {
                        audioDialogos.Stop();
                        timer += Time.deltaTime;
                        if (timer >= pauseBetween)
                        {
                            isWaiting = false;
                            ShowNextDialogue();
                        }
                    }
                }
                else
                {
                    panelDialogo.SetActive(false);
                    timer += Time.deltaTime;
                    if (timer >= pauseBetween)
                    {
                        isWaiting = false;
                        pause = true;
                        ShowNextDialogue();
                    }
                }
            }
        }
    }
}
