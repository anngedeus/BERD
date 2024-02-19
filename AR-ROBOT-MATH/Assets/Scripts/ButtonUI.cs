using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class ButtonUI : MonoBehaviour
{


    public void LeftButtonPressed()
    {
        Debug.Log("I'm left button pressed");
    }
    public void RightButtonPressed()
    {
        Debug.Log("I'm right button pressed");
    }

   
}
