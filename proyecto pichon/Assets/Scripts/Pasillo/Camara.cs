using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;

public class Camara : MonoBehaviour
{
    public TMP_Text distanceText;
   
    public Transform[] Camaras;
    public Transform player;
    public int distancia = 5;

    Vector3 start_pos;
    Vector3 end_pos;

    // Start is called before the first frame update
    void Start()
    {
        //start_pos = Object1.transform.position;
        //end_pos = new Vector3(Object1.transform.position.x + 10, Object1.transform.position.y, Object1.transform.position.z +10);
        //Object1.transform.position = Vector3.Lerp(start_pos, end_pos, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var camara in Camaras)
        {
            //Debug.Log(Vector3.Distance(camara.position, Object2.position));
            //distanceText.text = (camara.position - Object2.position).magnitude.ToString();

            if ((camara.position - player.position).magnitude < distancia)
            {
                start_pos = camara.transform.position;
                end_pos = new Vector3(camara.transform.position.x, camara.transform.position.y, 0);
                camara.transform.position = Vector3.Lerp(start_pos, end_pos, 0.05f);
                camara.GetChild(0).gameObject.SetActive(true);
            }
        }  

        if(DetachFromPanelEvent aunmentar)
                time+= Time.deltaTime;
        else {}
   
    }
   
}
