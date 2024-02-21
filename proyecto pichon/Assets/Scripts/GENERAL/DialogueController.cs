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

    public AudioClip[] voces;


    public void StartDialogue(List<string> dialogs, float pauseDuration)
    {
        isShowingText = true; //nuevo
        dialogList = dialogs;
        currentDialog = 0;
        pauseBetween = pauseDuration;
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
                if (currentText.Equals("El �mbito") || currentText.Equals("Pelko") || currentText.Equals("P�nico esc�nico") || currentText.Equals("Enoclofobia"))
                {
                    switch (currentText)
                    {
                        case "El �mbito":
                            break;
                        case "Pelko":
                            break;
                        case "P�nico esc�nico":
                            break;
                        case "Enoclofobia":
                            break;
                    }
                    panelDialogo.SetActive(true);
                    nombre.text = currentText;
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
