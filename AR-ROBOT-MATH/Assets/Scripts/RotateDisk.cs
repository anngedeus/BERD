using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class RotateDisk : MonoBehaviour
{
    private float rotationSpeed = 50f;
    private int rotationCount = 0;
    private bool isRotating = false;
    private Vector2 lastTouchPosition;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isRotating = true;
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && isRotating)
            {
                Vector2 deltaTouchPosition = touch.position - lastTouchPosition;
                float rotationAmount = -deltaTouchPosition.x * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount);
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isRotating = false;
            }
        }
    }

    void LateUpdate()
    {
     
        if (Mathf.Abs(transform.rotation.eulerAngles.y) < 0.01f)
        {
            rotationCount++;
            Debug.Log("Rotation Count: " + rotationCount);
        }
    }
}
