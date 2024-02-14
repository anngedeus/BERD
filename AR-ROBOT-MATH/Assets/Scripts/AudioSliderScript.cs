using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliderScript : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = audioSource.volume;
    }

    public void AdjustVolume()
    {
        // Adjust the volume of the audio source based on the slider's value
        audioSource.volume = volumeSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
