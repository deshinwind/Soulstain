using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.XR.Interaction.Toolkit;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogText;
    
    public float delay = 0.1f;

    public bool encenderLuz;

    public Vector3 alphaPanel;
    
    public GameObject panelSolido;

    public GameObject player;

    private float pauseBetween = 1f;
    private float timer;

    private List<string> dialogList = new List<string>();

    private int currentLetterIndex = 0;
    private int currentDialog = 0;
    
    private string currentText;
    
    private bool isShowingText = false;
    private bool isWaiting = false;

    private void Start()
    {
        alphaPanel = new Vector3 (1, 0, 0);
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
            if (dialogList[currentDialog].Equals(""))
            {
                encenderLuz = true;
                Invoke("DesactivarPanel", 2f);
                player.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
                player.GetComponent<ActionBasedContinuousTurnProvider>().enabled = true;
                //A PARTIR DE AQUI EL JUGADOR PUEDE MOVERSE
            }
                
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

    public void DesactivarPanel()
    {
        panelSolido.gameObject.SetActive(false);
    }

    private void Update()
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
                ShowNextDialogue();
            }
        }
    }

    private void LateUpdate()
    {
        if (encenderLuz)
        {
            alphaPanel = Vector3.Lerp(alphaPanel, new Vector3(0f, 0f, 0f), 0.001f);
            panelSolido.GetComponent<Image>().color = new Color(panelSolido.GetComponent<Image>().color.r, panelSolido.GetComponent<Image>().color.g, panelSolido.GetComponent<Image>().color.b, alphaPanel.x);
        }
    }
}