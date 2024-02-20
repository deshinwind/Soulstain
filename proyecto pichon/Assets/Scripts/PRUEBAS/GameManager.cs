using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public GameObject puerta;
    public GameObject panelSolido;
    public GameObject panelDialogos;

    public bool abrirPuerta;

    public Vector3 alphaPanel = new Vector3(0, 0, 0);
    public Vector3 rotacion = new Vector3(0, -133, 0);

    public List<string> dialogs = new List<string>();

    public DialogueManager dialogueManager;

    public float wait = 2f;

    void Start()
    {
        dialogueManager.StartDialogue(dialogs, wait);
    }

    private void LateUpdate()
    {
        if (abrirPuerta)
        {
            puerta.transform.rotation = Quaternion.Euler(Vector3.Lerp(puerta.transform.rotation.eulerAngles, rotacion, 0.0025f));
            alphaPanel = Vector3.Lerp(alphaPanel, new Vector3(1f, 0f, 0f), 0.05f);
            panelSolido.GetComponent<Image>().color = new Color(panelSolido.GetComponent<Image>().color.r, panelSolido.GetComponent<Image>().color.g, panelSolido.GetComponent<Image>().color.b, alphaPanel.x);
        }
    }
    public void AbrirPuerta()
    {
        panelSolido.gameObject.SetActive(true);
        panelDialogos.gameObject.SetActive(false);
        panelSolido.GetComponent<Image>().color = Color.white;
        abrirPuerta = true;
        Invoke("Transicion", 3f);
    }

    public void Transicion()
    {
        SceneManager.LoadScene("Plato");
    }
}