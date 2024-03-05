using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioSliderScript : MonoBehaviour
{
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
    }

    void Update()
    {
        switch (parameter)
        {
            case AudioParameter.Volume:
                audioSource.volume = slider.value;
                break;
            case AudioParameter.Pitch:
                audioSource.pitch = slider.value;
                break;
            default:
                Debug.LogWarning("Unhandled AudioParameter: " + parameter);
                break;
        }
    }
}
