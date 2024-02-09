using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public GameObject puerta;
    public GameObject panel;
    public bool abrirPuerta;
    public Vector3 alphaPanel = new Vector3(0, 0, 0);

    public Vector3 rotacion = new Vector3(0, -133, 0);

    public List<string> dialogs = new List<string>();
    public DialogueManager dialogueManager;
    public float wait = 2f;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager.StartDialogue(dialogs, wait);
    }

    private void LateUpdate()
    {
        if (abrirPuerta)
        {
            puerta.transform.rotation = Quaternion.Euler(Vector3.Lerp(puerta.transform.rotation.eulerAngles, rotacion, 0.00075f));
            alphaPanel = Vector3.Lerp(alphaPanel, new Vector3(1f, 0f, 0f), 0.01f);
            panel.GetComponent<Image>().color = new Color(panel.GetComponent<Image>().color.r, panel.GetComponent<Image>().color.g, panel.GetComponent<Image>().color.b, alphaPanel.x);
        }
    }
    public void AbrirPuerta()
    {
        panel.GetComponent<Image>().color = Color.white;
        abrirPuerta = true;
    }
}