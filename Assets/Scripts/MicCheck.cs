using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicCheck : MonoBehaviour
{

    public bool isUsingMic;
    private bool hasMic;
    private AudioSource mic;
    private AudioClip clip;
    private float[] spectrum;

    // Sample data
    public int sampleDataLength = 2048;
    private float[] samples;

    void Start()
    {
        spectrum = new float[2048];
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
        mic.clip = Microphone.Start(Microphone.devices[0].ToString(), true, 300, AudioSettings.outputSampleRate);
        clip = mic.clip;
        mic.Play();
        GetSampleData();
    }


    private void GetSampleData(){
        samples = new float[sampleDataLength];
        clip.GetData(samples, 0);
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            Debug.Log(spectrum[i]);
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        }
    }
}
