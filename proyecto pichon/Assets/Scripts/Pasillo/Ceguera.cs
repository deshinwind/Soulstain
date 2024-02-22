using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceguera : MonoBehaviour
{
    private Animator animator;




    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void Cieguisimo()
    {
        Invoke("UI", 0.8f);
        
    } 

    public void UI()
    {
        animator.SetTrigger("Ciego");
    }
}
