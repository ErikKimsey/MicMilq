using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicCheck : MonoBehaviour
{
    
    private AudioSource mic;
    void Start()
    {
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }
        HasMic();
    }

    private void HasMic(){
        if(Microphone.devices.Length > 0){
            SetMicAsSource();
        } else {
            Debug.Log("no device");
        }
    }

    private void SetMicAsSource(){
            mic = GetComponent<AudioSource>();
            mic.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
            mic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
