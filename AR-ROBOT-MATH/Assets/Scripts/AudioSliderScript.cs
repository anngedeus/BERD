using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioSliderScript : MonoBehaviour
{
    /*public AudioSource audioSource;
    public Slider volumeSlider;
    // Start is called before the first frame update
    private void Start()
    {

    }

     //Update is called once per frame
    void Update()
    {
        audioSource.volume = volumeSlider.value;
    }*/
  
    public enum AudioParameter
    {
        Volume,
        Pitch
    }

    [SerializeField]
    public AudioParameter parameter;
    public AudioSource audioSource;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void Update()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        switch (parameter)
        {
            case AudioParameter.Volume:
                audioSource.volume = value;
                break;
            case AudioParameter.Pitch:
                audioSource.pitch = value;
                break;
            default:
                Debug.LogWarning("Unhandled AudioParameter: " + parameter);
                break;
        }
    }
}
