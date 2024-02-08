using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
}