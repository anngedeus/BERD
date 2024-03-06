using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneScalingPosition : MonoBehaviour
{


    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        Vector3 deviceRotation = Input.acceleration;

        if (Mathf.Abs(deviceRotation.x) > Mathf.Abs(deviceRotation.y))
        {
            transform.localScale = initialScale * 1.5f;
        }
        else
        {
            transform.localScale = initialScale * 0.5f;
        }
    }


}
