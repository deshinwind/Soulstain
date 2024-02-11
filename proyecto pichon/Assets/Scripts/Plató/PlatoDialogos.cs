using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlatoDialogos : MonoBehaviour
{
    public TMP_Text dialogText;

    public float delay = 0.1f;

    public Vector3 alphaPanel;

    public GameObject panelDialogo;
    public GameObject player;

    private float pauseBetween = 1f;
    private float timer;

    private List<string> dialogList = new List<string>();

    private int currentLetterIndex = 0;
    private int currentDialog = 0;

    private string currentText;

    private bool isShowingText = false;
    private bool isWaiting = false;
    public bool pause = false;

    private void Start()
    {
        alphaPanel = new Vector3(1, 0, 0);
    }

    public void StartDialogue(List<string> dialogs, float pauseDuration)
    {
        dialogList = dialogs;
        currentDialog = 0;
        pauseBetween = pauseDuration;
        ShowNextDialogue();
    }

    public void ShowNextDialogue()
    {
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
            //dialogText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!pause)
        {
            if (currentText != null)
            {
                if (!currentText.Equals(""))
                {
                    if (isShowingText)
                    {
                        timer += Time.deltaTime;
                        if (timer > delay)
                        {
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

    private void LateUpdate()
    {

    }
}
