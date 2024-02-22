using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class animatedhandoninput : MonoBehaviour
{
    public InputActionProperty pinchanimation;
    public InputActionProperty gripanimation;
    public Animator handanimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchanimation.action.ReadValue<float>();
        handanimator.SetFloat("Trigger", triggerValue);


       float gripValue = gripanimation.action.ReadValue<float>();
        handanimator.SetFloat("Grip", gripValue);
    }
}
