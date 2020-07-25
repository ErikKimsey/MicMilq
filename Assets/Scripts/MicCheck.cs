using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicCheck : MonoBehaviour
{

    public bool isUsingMic;
    private bool hasMic;
    private AudioSource mic;
    private AudioClip clip;
    private float[] samples;

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
            hasMic = true;
            SetMicAsSource();
        } else {
            hasMic = false;
            Debug.Log("no device");
        }
    }

    private void SetMicAsSource(){
        mic = GetComponent<AudioSource>();
        mic.clip = Microphone.Start(Microphone.devices[0].ToString(), true, 10, AudioSettings.outputSampleRate);
        clip = mic.clip;
        mic.Play();
        GetSampleData();
    }


    private void GetSampleData(){
        samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);
        // foreach(var s in samples){
        //     Debug.Log(s);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // if(hasMic){
        //     // Debug.Log(clip.frequency);s
        // }
    }
}
