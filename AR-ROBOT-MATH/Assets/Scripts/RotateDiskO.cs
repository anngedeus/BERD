using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class RotateDiskO : MonoBehaviour
{
    private Touch touch;
    private Vector3 oldTouchPosition;
    private Vector3 NewTouchPosition;
    private int counter;
    [SerializeField]
    private float keepRotateSpeed = 10f;
    private float rotationAngle;

    private void Update()
    {
        RotateThings();
    }
    private void RotateThings()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                oldTouchPosition = touch.position;
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                NewTouchPosition = touch.position;
                rotationAngle = (NewTouchPosition.z - oldTouchPosition.z) * keepRotateSpeed;
                transform.Rotate(Vector3.up, rotationAngle, Space.World);
                oldTouchPosition = NewTouchPosition;
            }

            Vector3 rotDirection = oldTouchPosition - NewTouchPosition;
            Debug.Log(rotDirection);
            if (rotDirection.z < 0)
            {
                RotateRight();
            }
            else if (rotDirection.z > 0)
            {
                RotateLeft();
            }
        }
    }

    void RotateLeft()
    {
        transform.rotation = Quaternion.Euler(0f, 1.5f * keepRotateSpeed, 0f) * transform.rotation;
        counter--;
        Debug.Log("I'm decreasing");
    }

    void RotateRight()
    {
        transform.rotation = Quaternion.Euler(0f, -1.5f * keepRotateSpeed, 0f) * transform.rotation;
        counter++;
        Debug.Log("I'm increasing");
    }

}
