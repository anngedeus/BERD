using UnityEngine;
using TMPro;

public class RotateDiskLeft : MonoBehaviour
{
    private Touch touch;
    private float oldTouchPosition;
    public TMP_Text leftText;
    private int randomNumber;
    private bool isDragging = false;

    private void Start()
    {
        RandomGenerator();
    }

    private void RandomGenerator()
    {
        randomNumber = Random.Range(2, 13);
        leftText.text = randomNumber.ToString();
    }

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
                oldTouchPosition = touch.position.x;
                isDragging = true;
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                if (isDragging)
                {
                    float swipeValue = touch.position.x - oldTouchPosition;
                    if (swipeValue < 0)
                    {
                        //Disk will rotate right
                        transform.Rotate(Vector3.forward, swipeValue * 1.5f, Space.World);
                        randomNumber++;
                        if (randomNumber > 12)
                        {
                            randomNumber = 2;
                        }
                        leftText.text = randomNumber.ToString();
                    }
                    else if (swipeValue > 0)
                    {
                        //Disk will rotate left
                        transform.Rotate(Vector3.forward, swipeValue * 1.5f, Space.World);
                        randomNumber--;
                        if (randomNumber < 2)
                        {
                            randomNumber = 12;
                        }
                        leftText.text = randomNumber.ToString();
                    }

                    oldTouchPosition = touch.position.x;
                }
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                isDragging = false;
            }
        }
    }
}
