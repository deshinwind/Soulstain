using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CamaraFlash : MonoBehaviour
{
    public float distance = 5f;
    public float speed = 5f;
    private Transform player;
    private Bounds bounds;
    private float movimientoZ;
    private Animator animator;
    public ControladorPasillo controladorPasillo;
    private Ceguera ceguera;
    public float DañoTemp = 0;

    public bool camaraActiva = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        bounds = GameObject.FindWithTag("AreaCamara").GetComponent<BoxCollider>().bounds;
        movimientoZ = Random.Range(bounds.min.z, bounds.max.z);
        animator = gameObject.GetComponent<Animator>();
        ceguera = GameObject.Find("imagenciego").gameObject.GetComponent<Ceguera>();


    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.LookAt(player.transform);
        if (Vector3.Distance(transform.position, player.position) <= distance)
        {
            camaraActiva = true;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, movimientoZ), speed * Time.deltaTime);
            animator.enabled = true;
            DañoTemp += Time.deltaTime;
            Invoke("Flash", 2.25f);

        }
    }


    public void Flash()
    {
        animator.SetTrigger("Flash");
        ceguera.Cieguisimo();
        if (DañoTemp > 2.8)
        {
            DañoTemp = 0;
            controladorPasillo.sumarpuntos();
        }

    }


}