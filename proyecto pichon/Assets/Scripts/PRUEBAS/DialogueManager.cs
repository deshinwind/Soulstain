using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogText;
    public float delay = 0.1f;

    private float pauseBetween = 1f;
    private List<string> dialogList = new List<string>();
    private int currentDialog = 0;
    private string currentText;
    private float timer;
    private int currentLetterIndex = 0;
    private bool isShowingText = false;
    private bool isWaiting = false;


    public Vector3 alphaPanel;
    public bool encenderLuz;
    public GameObject panel;

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
            panel.GetComponent<Image>().color = new Color(panel.GetComponent<Image>().color.r, panel.GetComponent<Image>().color.g, panel.GetComponent<Image>().color.b, alphaPanel.x);
        }
    }
}