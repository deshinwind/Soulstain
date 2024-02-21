using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bate : MonoBehaviour
{

    public bool isgrab = false;
    public float delay = 2f;

    public void agarrao()
    {
        Invoke("Delay", delay);
    }

    public void noagarrao()
    {
        isgrab = false;
    }

    public void Delay()
    {
        isgrab = true;
    }

}
