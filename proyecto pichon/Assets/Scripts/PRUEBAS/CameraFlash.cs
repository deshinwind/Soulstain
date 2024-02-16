using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlash : MonoBehaviour
{
    public float distance = 5f;
    public float speed = 5f;
    private GameObject Aviso;
    private GameObject flash;
    private Transform player;
    private Bounds bounds;
    private float movimientoZ;
    private Animator animator;
    public bool ceguera = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        bounds = GameObject.FindWithTag("AreaCamara").GetComponent<BoxCollider>().bounds;
        flash = gameObject.transform.GetChild(0).gameObject;
        Aviso = gameObject.transform.GetChild(1).gameObject;
        movimientoZ = Random.Range(bounds.min.z, bounds.max.z);
        animator = gameObject.GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, player.position));
        if(Vector3.Distance(transform.position, player.position) <= distance) 
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, movimientoZ), speed * Time.deltaTime);
            animator.enabled = true;
            Invoke("Flash", 2.25f);
            //LuzRoja();
        }
        
    }

    /*public void LuzRoja()
    {
        if (!ceguera)
        {
            flash.SetActive(false);
            Aviso.SetActive(true);
            Invoke("Flash", 2.25f);
        }
    }*/

    public void Flash()
    {
        animator.SetTrigger("Flash");
        //Aviso.SetActive(false);
        //flash.SetActive(true);
        //Invoke("apaga", 1f);
    }

    public void apaga()
    {
        animator.SetTrigger("Flash");
    }
}
