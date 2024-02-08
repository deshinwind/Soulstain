using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<string> dialogs = new List<string>();
    public DialogueManager dialogueManager;
    public float wait = 2f;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager.StartDialogue(dialogs, wait);
    }
}