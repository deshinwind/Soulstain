using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Efectos : MonoBehaviour
{
    public Volume volume;

    public Bloom bloom = null;

    // Start is called before the first frame update
    void Start()
    {
        volume.sharedProfile.TryGet<Bloom>(out bloom);
        bloom.active = true;
        Debug.Log(bloom.threshold.overrideState);
        bloom.threshold.overrideState = true;
        //bloom.threshold.value = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
