using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class animatedhandoninput : MonoBehaviour
{
    public InputActionProperty pinchanimationAction;
    public InputActionProperty gripanimationAction;
    public Animator handanimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchanimationAction.action.ReadValue<float>();
        handanimator.SetFloat("Trigger", triggerValue);


       float gripValue = gripanimationAction.action.ReadValue<float>();
        handanimator.SetFloat("Grip", gripValue);
    }
}
